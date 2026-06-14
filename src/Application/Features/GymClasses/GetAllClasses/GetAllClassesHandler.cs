using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Application.Common.DTOs;


namespace Application.Features.GymClasses.GetAllClasses;

public sealed class GetAllClassesHandler
    : IQueryHandler<GetAllClassesQuery, List<GymClassCardDto>>
{
    private readonly IGymClassRepository _repo;

    public GetAllClassesHandler(IGymClassRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<GymClassCardDto>> Handle(GetAllClassesQuery query, CancellationToken ct)
    {
        var classes = await _repo.GetAllAsync(ct);

        return classes.Select(x => new GymClassCardDto(
            x.Id,
            x.Name,
            x.Instructor,
            x.StartTime,
            x.Capacity,
            x.Category
        )).ToList();
    }
}