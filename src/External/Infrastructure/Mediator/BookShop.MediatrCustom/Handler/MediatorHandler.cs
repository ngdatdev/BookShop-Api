using BookShop.Application.Shared.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookShop.MediatrCustom.Handler;

public class MediatorHandler : IMediator
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MediatorHandler> _logger;

    public MediatorHandler(IServiceProvider serviceProvider, ILogger<MediatorHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<TResponse> SendAsync<TResponse>(
        IFeatureRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
        where TResponse : class, IFeatureResponse
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Handling request of type {RequestType}", request.GetType());

        try
        {
            var handler = _serviceProvider.GetService<IFeatureHandler<IFeatureRequest<TResponse>, TResponse>>();
            if (handler == null)
                throw new InvalidOperationException(
                    $"Handler for '{typeof(IFeatureRequest<TResponse>).Name}' not registered."
                );

            _logger.LogDebug("Found handler of type {HandlerType}", handler.GetType());

            var response = await handler.HandlerAsync(request, cancellationToken);

            if (response == null)
                throw new InvalidOperationException(
                    $"Handler for '{typeof(IFeatureRequest<TResponse>).Name}' returned null response."
                );

            _logger.LogInformation(
                "Request handled successfully with response of type {ResponseType}",
                response.GetType()
            );

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred while handling request of type {RequestType}",
                request.GetType()
            );
            throw;
        }
    }
}
