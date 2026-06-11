using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Application.Common.DTOs;

namespace Application.Features.Memberships.GetMembership;

public sealed class GetMembershipHandler
    : IQueryHandler<GetMembershipQuery, MembershipDto?>
{
    private readonly IMembershipRepository _membershipRepository;

    public GetMembershipHandler(
        IMembershipRepository membershipRepository)
    {
        _membershipRepository = membershipRepository;
    }

    public async Task<MembershipDto?> Handle(
        GetMembershipQuery query,
        CancellationToken ct)
    {
        var membership = await _membershipRepository
            .GetByUserIdAsync(query.UserId, ct);

        if (membership is null)
            return null;

        return new MembershipDto(
            membership.Id,
            membership.UserId,
            membership.MembershipType,
            membership.StartDate,
            membership.IsActive);
    }
}