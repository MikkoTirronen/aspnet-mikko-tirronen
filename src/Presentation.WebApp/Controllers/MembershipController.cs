using System.Security.Claims;
using Application.Common.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class MembershipController(IMembershipService membershipService) : Controller
{
    private readonly IMembershipService _membershipService = membershipService;

    public async Task<IActionResult> Index(CancellationToken ct)
{
    var userId = User.FindFirstValue(
        ClaimTypes.NameIdentifier);

    var membership =
        await _membershipService.GetMembershipAsync(
            userId!,
            ct);

    var vm = new MembershipIndexViewModel
    {
        CurrentMembership = membership,

        Plans =
        [
            new()
            {
                Name = "Basic",
                Price = 299,
                Description = "Perfect for getting started.",
                Features =
                [
                    "Gym Access",
                    "Locker Room",
                    "Mobile App"
                ]
            },

            new()
            {
                Name = "Premium",
                Price = 499,
                Description = "Most popular plan.",
                Features =
                [
                    "Gym Access",
                    "Group Classes",
                    "Nutrition Plan"
                ]
            },

            new()
            {
                Name = "Elite",
                Price = 799,
                Description = "Everything included.",
                Features =
                [
                    "Gym Access",
                    "Classes",
                    "Personal Trainer"
                ]
            }
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
        string membershipType, CancellationToken ct)
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        var success =
            await _membershipService
                .CreateMembershipAsync(
                    userId!,
                    membershipType, ct);

        if (!success)
        {
            ModelState.AddModelError(
                "",
                "Membership already exists");
        }

        return RedirectToAction(nameof(Index));
    }
}