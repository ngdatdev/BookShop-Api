using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.CartItems.AddItemToCart;

/// <summary>
///     AddItemToCart Response
/// </summary>
public class AddItemToCartResponse : IFeatureResponse
{
    public AddItemToCartResponseStatusCode StatusCode { get; init; }
}
