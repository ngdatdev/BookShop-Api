namespace BookShop.Application.Shared.Features;

/// <summary>
///     Marker interface to represent a request with a response
/// </summary>
public interface IFeatureRequest<out TResponse>
    where TResponse : class, IFeatureResponse { }
