using Microsoft.Extensions.DependencyInjection;
using Application.Abstractions.Commands;
namespace Application.Common.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _provider;

    public CommandDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken ct)
    {
        var handlerType = typeof(ICommandHandler<,>)
            .MakeGenericType(command.GetType(), typeof(TResponse));

        dynamic handler = _provider.GetRequiredService(handlerType);

        return handler.Handle((dynamic)command, ct);
    }
}