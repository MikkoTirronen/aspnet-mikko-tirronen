namespace Infrastructure.Dispatchers;

using Microsoft.Extensions.DependencyInjection;
using Application.Abstractions.Queries;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(
        IQuery<TResponse> query,
        CancellationToken ct)
    {
        var handlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(query.GetType(), typeof(TResponse));

        dynamic handler = _serviceProvider.GetRequiredService(handlerType);

        return await handler.Handle((dynamic)query, ct);
    }
}