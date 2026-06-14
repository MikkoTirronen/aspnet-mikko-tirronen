using Application.Abstractions.Commands;
using Application.Common.Results;

namespace Application.Features.GymClasses.UpdateGymClass;

public sealed record UpdateGymClassCommand(
    Guid Id,
    string Name,
    string Instructor,
    DateTime StartTime,
    int Capacity,
    string Category
) : ICommand<Result<Guid>>;