using Application.Abstractions.Queries;
using Application.Common.DTOs;

namespace Application.Features.GymClasses.GetClassDetails;

public sealed record GetClassDetailsQuery(
    Guid ClassId,
    string UserId
) : IQuery<GymClassDetailsDto?>;