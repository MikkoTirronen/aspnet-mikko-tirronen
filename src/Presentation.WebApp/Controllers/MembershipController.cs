using System.Security.Claims;
using Application.Abstractions.Commands;
using Application.Abstractions.Queries;
using Application.Common.Interfaces;
using Application.Features.Memberships.CreateMembership;
using Application.Features.Memberships.GetMembership;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class MembershipController : Controller
{
    private readonly IQueryDispatcher _queries;
    private readonly ICommandDispatcher _commands;

    public MembershipController(
        IQueryDispatcher queries,
        ICommandDispatcher commands)
    {
        _queries = queries;
        _commands = commands;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var membership = await _queries.Send(
            new GetMembershipQuery(userId),
            ct);

        var vm = new MembershipIndexViewModel
        {
            CurrentMembership = membership,

            Plans =
            [
                new() { Name = "Basic", Price = 299, Description = "Perfect for getting started." },
                new() { Name = "Premium", Price = 499, Description = "Most popular plan." },
                new() { Name = "Elite", Price = 799, Description = "Everything included." }
            ]
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
        string membershipType,
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