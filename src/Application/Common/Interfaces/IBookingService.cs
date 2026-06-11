namespace Application.Common.Interfaces;

public interface IBookingService
{
    Task BookAsync(Guid classId, Guid userId, CancellationToken ct);

    Task CancelAsync(Guid classId, Guid userId, CancellationToken ct);
}