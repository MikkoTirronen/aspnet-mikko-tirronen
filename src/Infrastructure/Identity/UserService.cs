using Application.Abstractions.Services;
using Application.Common.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string?> GetEmailAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user?.Email;
    }

    public async Task<string?> GetFullNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user?.FullName;
    }

    public async Task<AppUserDto?> GetByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            throw new Exception("User not found");

        return new AppUserDto
        {
            Id = user.Id ?? "",
            Email = user.Email ?? "",
            FullName = user.FullName ?? ""
        };

    }

    public async Task<bool> DeleteUserAsync(string userId)
{
    var user = await _userManager.FindByIdAsync(userId);

    if (user is null)
        return false;

    var result = await _userManager.DeleteAsync(user);

    return result.Succeeded;
}
}