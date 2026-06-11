namespace Application.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResponse> Send<TResponse>(
        IQuery<TResponse> query,
        CancellationToken ct);
}