using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.Shared.Features;

/// <summary>
///     Interface for handling feature requests.
/// </summary>
/// <typeparam name="TRequest">The type of request.</typeparam>
/// <typeparam name="TResponse">The type of response.</typeparam>
public interface IFeatureHandler<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class, IResponse
{
    /// <summary>
    ///     Handles the specified request and returns the response.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <returns>The response.</returns>
    Task<TResponse> HandlerAsync(TRequest request, CancellationToken cancellationToken);
}
