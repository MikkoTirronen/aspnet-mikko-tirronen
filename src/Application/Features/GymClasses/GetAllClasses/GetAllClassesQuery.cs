using Application.Abstractions.Queries;
using Application.Common.DTOs;

namespace Application.Features.GymClasses.GetAllClasses;

public sealed record GetAllClassesQuery()
    : IQuery<List<GymClassCardDto>>;