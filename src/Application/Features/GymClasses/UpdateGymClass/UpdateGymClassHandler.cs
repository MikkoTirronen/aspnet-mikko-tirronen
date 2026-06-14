using Application.Abstractions.Commands;
using Application.Abstractions.Repositories;
using Application.Abstractions.UnitOfWork;
using Application.Common.Results;

namespace Application.Features.GymClasses.UpdateGymClass;

public sealed class UpdateGymClassHandler
    : ICommandHandler<UpdateGymClassCommand, Result<Guid>>
{
    private readonly IGymClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public UpdateGymClassHandler(
        IGymClassRepository classRepo,
        IUnitOfWork uow)
    {
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(UpdateGymClassCommand command, CancellationToken ct)
    {
        var gymClass = await _classRepo.GetByIdAsync(command.Id, ct);

        if (gymClass is null)
            return Result<Guid>.Fail("Class not found.");

        try
        {
            gymClass.UpdateDetails(
                command.Name,
                command.Instructor,
                command.StartTime,
                command.Capacity,
                command.Category);

            await _uow.SaveChangesAsync(ct);

            return Result<Guid>.Ok(gymClass.Id);
        }
        catch (InvalidOperationException ex)
        {
            return Result<Guid>.Fail(ex.Message);
        }
    }
}