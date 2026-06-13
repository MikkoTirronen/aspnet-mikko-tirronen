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

        var FullName = user?.FirstName + " " + user?.LastName;
        return FullName;
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
            FirstName = user.FirstName ?? "",
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
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

    public async Task<bool> UpdateUserAsync(
    string userId,
    string firstName,
    string lastName,
    string email,
    string? phoneNumber)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return false;

        user.FirstName = firstName;
        user.LastName = lastName;
        user.PhoneNumber = phoneNumber;

        var setEmailResult = await _userManager.SetEmailAsync(user, email);

        if (!setEmailResult.Succeeded)
        {
            foreach (var error in setEmailResult.Errors)
            {
                Console.WriteLine($"SetEmail failed: {error.Code}: {error.Description}");
            }

            return false;
        }

        var setUserNameResult = await _userManager.SetUserNameAsync(user, email);

        if (!setUserNameResult.Succeeded)
        {
            foreach (var error in setUserNameResult.Errors)
            {
                Console.WriteLine($"SetUserName failed: {error.Code}: {error.Description}");
            }

            return false;
        }

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
            {
                Console.WriteLine($"Update failed: {error.Code}: {error.Description}");
            }

            return false;
        }

        return true;
    }

}