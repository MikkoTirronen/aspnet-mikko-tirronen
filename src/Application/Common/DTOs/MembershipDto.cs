using Domain.Enums;

namespace Application.Common.DTOs;

public sealed record MembershipDto(
    Guid Id,
    string UserId,
    MembershipType MembershipType,
    DateTime StartDate,
    bool IsActive
);