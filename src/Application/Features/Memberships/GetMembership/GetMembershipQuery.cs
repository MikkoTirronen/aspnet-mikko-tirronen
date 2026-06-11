using Application.Abstractions.Queries;
using Application.Common.DTOs;

namespace Application.Features.Memberships.GetMembership;

public sealed record GetMembershipQuery(string UserId) : IQuery<MembershipDto?>;