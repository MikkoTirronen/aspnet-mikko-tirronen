using Application.Abstractions.Commands;
using Application.Common.Results;

namespace Application.Features.GymClasses.DeleteGymClass;

public sealed record DeleteGymClassCommand(Guid Id) : ICommand<Result<Guid>>;