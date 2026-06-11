using Application.Abstractions.Commands;

namespace Application.Features.Memberships.CreateMembership;

public sealed record CreateMembershipCommand(
    string UserId,
    string MembershipType
) : ICommand<bool>;