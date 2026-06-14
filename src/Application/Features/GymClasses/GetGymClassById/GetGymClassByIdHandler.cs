using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;

namespace Application.Features.GymClasses.GetGymClassById;

public sealed class GetGymClassByIdHandler
    : IQueryHandler<GetGymClassByIdQuery, GymClassAdminDto?>
{
    private readonly IGymClassRepository _classRepo;

    public GetGymClassByIdHandler(
        IGymClassRepository classRepo)
    {
        _classRepo = classRepo;
    }

    public async Task<GymClassAdminDto?> Handle(
        GetGymClassByIdQuery query,
        CancellationToken ct)
    {
        var gymClass = await _classRepo.GetByIdAsync(
            query.Id,
            ct);

        if (gymClass is null)
            return null;

        return new GymClassAdminDto
        {
            Id = gymClass.Id,
            Name = gymClass.Name,
            Instructor = gymClass.Instructor,
            StartTime = gymClass.StartTime,
            Capacity = gymClass.Capacity,
            Category = gymClass.Category
        };
    }
}