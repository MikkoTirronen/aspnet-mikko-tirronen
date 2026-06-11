namespace Application.Common.DTOs;

public sealed record GymClassCardDto(
    Guid Id,
    string Name,
    string Instructor,
    DateTime StartTime,
    int Capacity,
    string Category
);