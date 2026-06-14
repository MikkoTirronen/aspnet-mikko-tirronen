using Application.Abstractions.Commands;
using Application.Abstractions.Repositories;
using Application.Abstractions.UnitOfWork;
using Domain.Entities;

namespace Application.Features.GymClasses.CreateGymClass;

public sealed class CreateGymClassHandler
    : ICommandHandler<CreateGymClassCommand, Guid>
{
    private readonly IGymClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public CreateGymClassHandler(
        IGymClassRepository classRepo,
        IUnitOfWork uow)
    {
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateGymClassCommand command, CancellationToken ct)
    {
        var gymClass = new GymClass(
            command.Name,
            command.Instructor,
            command.StartTime,
            command.Capacity,
            command.Category);

        await _classRepo.AddAsync(gymClass, ct);
        await _uow.SaveChangesAsync(ct);

        return gymClass.Id;
    }
}