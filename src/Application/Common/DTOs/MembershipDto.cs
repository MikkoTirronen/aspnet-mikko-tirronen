namespace Application.Common.DTOs;

public sealed record MembershipDto(
    Guid Id,
    string UserId,
    string MembershipType,
    DateTime StartDate,
    bool IsActive
);