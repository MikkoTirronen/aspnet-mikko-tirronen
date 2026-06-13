using System.Security.Claims;
using Application.Abstractions.Commands;
using Application.Abstractions.Queries;
using Application.Features.Profile.DeleteAccount;
using Application.Features.Profile.GetUserProfile;
using Application.Features.Profile.UpdateProfile;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IQueryDispatcher _dispatcher;
    private readonly ICommandDispatcher _handler;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ProfileController(IQueryDispatcher dispatcher, ICommandDispatcher handler, SignInManager<ApplicationUser> signInManager)
    {
        _dispatcher = dispatcher;
        _handler = handler;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var dto = await _dispatcher.Send(
            new GetUserProfileQuery(userId),
            ct);

        if (dto is null)
            return NotFound();

        var vm = new UserProfileViewModel
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,

            Membership = new MembershipViewModel
            {
                MembershipType = dto.MembershipType,
                IsActive = dto.HasActiveMembership,
                StartDate = dto.StartDate ?? DateTime.MinValue,
                EndDate = dto.EndDate ?? DateTime.MinValue
            },

            Bookings = dto.Bookings.Select(b => new BookingViewModel
            {
                BookingId = b.BookingId,
                ClassName = b.ClassName,
                StartTime = b.StartTime
            }).ToList()
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(
        UserProfileViewModel model,
        CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _handler.Send(
            new UpdateProfileCommand(
                userId,
                model.FirstName,
                model.LastName,
                model.Email,
                model.PhoneNumber??""),
            ct);

        if (!result)
        {
            ModelState.AddModelError("", "Could not update profile.");
            return View("Index", model);
        }

        TempData["Success"] = "Profile updated.";

        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAccount(
    CancellationToken ct)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _handler.Send(
            new DeleteAccountCommand(userId),
            ct);

        if (!result)
            return BadRequest();

        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}