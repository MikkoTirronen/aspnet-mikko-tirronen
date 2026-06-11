using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class ClassesController(IGymClassService gymClassService) : Controller
{
    private readonly IGymClassService _gymClassService = gymClassService;

    public async Task<IActionResult> Index(
        CancellationToken ct
    )
    {
        var classes = await _gymClassService.GetAllAsync(ct);

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

    [Authorize]
    public async Task<IActionResult> Details(Guid id, CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            throw new NullReferenceException("Login to see details");

        var gymClass = await _gymClassService.GetDetailsAsync(id, userId, ct);

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
            IsBookedByUser = gymClass.IsBookedByUser
        };

        return View(vm);
    }
}