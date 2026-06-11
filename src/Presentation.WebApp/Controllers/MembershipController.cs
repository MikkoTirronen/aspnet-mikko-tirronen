using System.Security.Claims;
using Application.Abstractions.Commands;
using Application.Abstractions.Queries;
using Application.Abstractions.Services;
using Application.Features.Memberships.CreateMembership;
using Application.Features.Memberships.GetMembership;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class MembershipController : Controller
{
    private readonly IQueryDispatcher _queries;
    private readonly ICommandDispatcher _commands;

    private readonly IMembershipPlanService _service;

    public MembershipController(
        IQueryDispatcher queries,
        ICommandDispatcher commands,
        IMembershipPlanService service)
    {
        _queries = queries;
        _commands = commands;
        _service = service;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var membership = await _queries.Send(
            new GetMembershipQuery(userId),
            ct);

        var plans = _service.GetPlans();

        var vm = new MembershipIndexViewModel
        {
            CurrentMembership = membership,

            Plans = plans.Select(p => new MembershipPlanViewModel
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Features = p.Features
            }).ToList()

        };

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        MembershipType membershipType,
        CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var success = await _commands.Send(
            new CreateMembershipCommand(userId, membershipType),
            ct);

        if (!success)
        {
            ModelState.AddModelError("", "Membership already exists");
            return View();
        }

        return RedirectToAction(nameof(Index));
    }
}