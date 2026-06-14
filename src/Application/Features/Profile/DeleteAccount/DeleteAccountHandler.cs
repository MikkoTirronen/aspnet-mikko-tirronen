using Application.Abstractions.Commands;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Abstractions.UnitOfWork;

namespace Application.Features.Profile.DeleteAccount;

public sealed class DeleteAccountHandler
    : ICommandHandler<DeleteAccountCommand, bool>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _uow;

    public DeleteAccountHandler(
        IBookingRepository bookingRepository,
        IMembershipRepository membershipRepository,
        IUserService userService,
        IUnitOfWork uow)
    {
        _bookingRepository = bookingRepository;
        _membershipRepository = membershipRepository;
        _userService = userService;
        _uow = uow;
    }

    public async Task<bool> Handle(
        DeleteAccountCommand command,
        CancellationToken ct)
    {
        await _bookingRepository.RemoveByUserIdAsync(
            command.UserId,
            ct);

        await _membershipRepository.RemoveByUserIdAsync(
            command.UserId,
            ct);

        await _uow.SaveChangesAsync(ct);

        return await _userService.DeleteUserAsync(
            command.UserId);
    }
}