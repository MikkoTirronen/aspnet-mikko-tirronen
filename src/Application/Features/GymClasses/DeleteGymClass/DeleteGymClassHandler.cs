using Application.Abstractions.Commands;
using Application.Abstractions.Repositories;
using Application.Abstractions.UnitOfWork;
using Application.Common.Results;

namespace Application.Features.GymClasses.DeleteGymClass;

public sealed class DeleteGymClassHandler
    : ICommandHandler<DeleteGymClassCommand, Result<Guid>>
{
    private readonly IGymClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public DeleteGymClassHandler(
        IGymClassRepository classRepo,
        IUnitOfWork uow)
    {
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(DeleteGymClassCommand command, CancellationToken ct)
    {
        var gymClass = await _classRepo.GetByIdAsync(command.Id, ct);

        if (gymClass is null)
            return Result<Guid>.Fail("Class not found.");

        await _classRepo.DeleteAsync(gymClass, ct);
        await _uow.SaveChangesAsync(ct);

        return Result<Guid>.Ok(command.Id);
    }
}