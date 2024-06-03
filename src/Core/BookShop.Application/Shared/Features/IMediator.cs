using System.Threading;
using System.Threading.Tasks;

namespace BookShop.Application.Shared.Features;

public interface IMediator
{
    Task<TResponse> SendAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default
    )
        where TResponse : class, IResponse
        where TRequest : class, IRequest<TResponse>;
}
