using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.Shared.Features;

/// <summary>
///     Interface for handling feature requests.
/// </summary>
public interface IFeatureHandler<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class, IResponse
{
    /// <summary>
    ///     Handles the specified request and returns the response.
    /// </summary>
    /// <param name="request">
    //      The request to handle.
    //  </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     The response.
    ///</returns>
    Task<TResponse> HandlerAsync(TRequest request, CancellationToken cancellationToken);
}
