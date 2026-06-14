using Application.Abstractions.Commands;
using Domain.Enums;

namespace Application.Features.Memberships.CreateMembership;

public sealed record CreateMembershipCommand(
    string UserId,
    MembershipType MembershipType
) : ICommand<bool>;