using System.Security.Claims;
using Application.Abstractions.Queries;
using Application.Features.Profile.GetUserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IQueryDispatcher _dispatcher;

    public ProfileController(IQueryDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var dto = await _dispatcher.Send(
            new GetUserProfileQuery(userId),
            ct);

        var vm = new UserProfileViewModel
        {
            FullName = dto.FullName,
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
}