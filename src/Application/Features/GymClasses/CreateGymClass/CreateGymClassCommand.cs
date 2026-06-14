using Application.Abstractions.Commands;

namespace Application.Features.GymClasses.CreateGymClass;

public sealed record CreateGymClassCommand(
    string Name,
    string Instructor,
    DateTime StartTime,
    int Capacity,
    string Category
) : ICommand<Guid>;