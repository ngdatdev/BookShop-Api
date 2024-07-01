namespace BookShop.Application.Features.CartItems.AddItemToCart;

/// <summary>
///     Extension Method for AddItemToCart features.
/// </summary>
public static class AddItemToCartExtensionMethod
{
    public static string ToAppCode(this AddItemToCartResponseStatusCode statusCode)
    {
        return $"{nameof(AddItemToCart)}Feature: {statusCode}";
    }
}
