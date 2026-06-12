using Application.Abstractions.Commands;

namespace Application.Features.Profile.DeleteAccount;

public sealed record DeleteAccountCommand(
    string UserId
) : ICommand<bool>;