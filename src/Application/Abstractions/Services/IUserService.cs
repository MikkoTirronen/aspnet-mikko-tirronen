using Application.Common.DTOs;

namespace Application.Abstractions.Services;
public interface IUserService
{
    Task<string?> GetEmailAsync(string userId);
    Task<string?> GetFullNameAsync(string userId);
    Task<AppUserDto?> GetByIdAsync(string userId);
    Task<bool> DeleteUserAsync(string userId);

}