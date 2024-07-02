using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Carts.ClearCart;

/// <summary>
///     ClearCart Response
/// </summary>
public class ClearCartResponse : IFeatureResponse
{
    public ClearCartResponseStatusCode StatusCode { get; init; }
}
