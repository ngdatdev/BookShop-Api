using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Application.Features.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default
    )
        where TRequest : class, IRequest<TResponse>
        where TResponse : class, IResponse
    {
        var handlerType = typeof(IFeatureHandler<,>).MakeGenericType(
            typeof(TRequest),
            typeof(TResponse)
        );
        var handler =
            (IFeatureHandler<TRequest, TResponse>)_serviceProvider.GetRequiredService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException(
                $"Handler for {typeof(TRequest)} is not registered"
            );
        }

        return await handler.HandlerAsync(request, cancellationToken);
    }
}
