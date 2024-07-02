namespace BookShop.Application.Features.Carts.ClearCart;

/// <summary>
///     Extension Method for ClearCart features.
/// </summary>
public static class ClearCartExtensionMethod
{
    public static string ToAppCode(this ClearCartResponseStatusCode statusCode)
    {
        return $"{nameof(ClearCart)}Feature: {statusCode}";
    }
}
