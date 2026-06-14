using Application.Abstractions.Commands;
using Application.Abstractions.Services;

namespace Application.Features.Profile.UpdateProfile;

public sealed class UpdateProfileHandler
    : ICommandHandler<UpdateProfileCommand, bool>
{
    private readonly IUserService _userService;

    public UpdateProfileHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(UpdateProfileCommand command, CancellationToken ct)
    {
        return await _userService.UpdateUserAsync(
            command.UserId,
            command.FirstName,
            command.LastName,
            command.Email,
            command.PhoneNumber);
    }
}