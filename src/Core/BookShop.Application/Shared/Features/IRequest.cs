namespace BookShop.Application.Shared.Features;

/// <summary>
///     Marker interface to represent a request with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface IRequest<out TResponse>
    where TResponse : class, IResponse { }
