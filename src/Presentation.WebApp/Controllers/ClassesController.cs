using System.Security.Claims;
using Application.Abstractions.Queries;
using Application.Common.Interfaces;
using Application.Features.GymClasses.GetAllClasses;
using Application.Features.GymClasses.GetClassDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class ClassesController : Controller
{
    private readonly IQueryDispatcher _queries;

    public ClassesController(IQueryDispatcher queries)
    {
        _queries = queries;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var classes = await _queries.Send(
            new GetAllClassesQuery(),
            ct);

        var vm = new GymClassesViewModel
        {
            Classes = classes.Select(x => new GymClassCardViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Instructor = x.Instructor,
                StartTime = x.StartTime,
                Capacity = x.Capacity,
                Category = x.Category
            }).ToList()
        };

        return View(vm);
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken ct)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdString is null)
            return Unauthorized();

        var gymClass = await _queries.Send(
            new GetClassDetailsQuery(id, userIdString),
            ct);

        if (gymClass is null)
            return NotFound();

        var vm = new GymClassDetailsViewModel
        {
            Id = gymClass.Id,
            Name = gymClass.Name,
            Instructor = gymClass.Instructor,
            StartTime = gymClass.StartTime,
            Capacity = gymClass.Capacity,
            Category = gymClass.Category,
            BookedCount = gymClass.BookedCount,
            IsBookedByUser = gymClass.IsBookedByUser,
            BookingId = gymClass.BookingId
        };

        return View(vm);
    }
}