using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Presentation.WebApp.Controllers;

public class BookingController : Controller
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [Authorize]
    public async Task<IActionResult> Create(Guid id, CancellationToken ct)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdString is null)
            return Unauthorized();

        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();

        await _bookingService.BookAsync(id, userId, ct);

        return RedirectToAction("Index", "Classes");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _bookingService.CancelAsync(id, userId, ct);

        return RedirectToAction("Details", "Classes", new { id });
    }
}