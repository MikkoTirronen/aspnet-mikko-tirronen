using Application.Abstractions.Commands;

namespace Application.Features.Profile.UpdateProfile;

public sealed record UpdateProfileCommand(
    string UserId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
) : ICommand<bool>;