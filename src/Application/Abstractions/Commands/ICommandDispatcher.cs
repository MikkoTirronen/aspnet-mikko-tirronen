namespace Application.Abstractions.Commands;

public interface ICommandDispatcher
{
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken ct);
}