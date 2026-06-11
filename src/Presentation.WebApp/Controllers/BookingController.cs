using System.Security.Claims;
using Application.Abstractions.Commands;
using Application.Features.Bookings.BookClass;
using Application.Features.Bookings.CancelBooking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Presentation.WebApp.Controllers;

public class BookingController : Controller
{
    private readonly ICommandDispatcher _dispatcher;

    public BookingController(ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [Authorize]
    public async Task<IActionResult> Create(Guid id, CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        await _dispatcher.Send(new BookClassCommand(id, userId), ct);

        return RedirectToAction("Index", "Classes");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
    {

        var bookingId = id;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var result = await _dispatcher.Send(new CancelBookingCommand(bookingId, userId), ct);

        return RedirectToAction("Details", "Classes", new { id = result.Value });
    }
}