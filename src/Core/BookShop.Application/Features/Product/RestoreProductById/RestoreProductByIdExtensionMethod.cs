namespace BookShop.Application.Features.Product.RestoreProductById;

/// <summary>
///     Extension Method for RestoreProductById features.
/// </summary>
public static class RestoreProductByIdExtensionMethod
{
    public static string ToAppCode(this RestoreProductByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RestoreProductById)}Feature: {statusCode}";
    }
}
