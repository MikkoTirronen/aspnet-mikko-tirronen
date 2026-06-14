using Application.Abstractions.Queries;

namespace Application.Features.GymClasses.GetGymClassById;

public sealed record GetGymClassByIdQuery(Guid Id)
    : IQuery<GymClassAdminDto?>;