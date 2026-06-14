using Application.Abstractions.Commands;
using Application.Abstractions.Repositories;
using Domain.Entities;

namespace Application.Features.Memberships.CreateMembership;
public sealed class CreateMembershipHandler
    : ICommandHandler<CreateMembershipCommand, bool>
{
    private readonly IMembershipRepository _repo;

    public CreateMembershipHandler(IMembershipRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(CreateMembershipCommand command, CancellationToken ct)
    {
        var exists = await _repo.ExistsByUserIdAsync(command.UserId, ct);

        if (exists)
            return false;

        var membership = new Membership(
            command.UserId,
            command.MembershipType);

        await _repo.AddAsync(membership, ct);

        return true;
    }
}