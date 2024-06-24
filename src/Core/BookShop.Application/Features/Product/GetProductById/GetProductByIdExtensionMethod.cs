namespace BookShop.Application.Features.Product.GetProductById;

/// <summary>
///     Extension Method for GetProductById features.
/// </summary>
public static class GetProductByIdExtensionMethod
{
    public static string ToAppCode(this GetProductByIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetProductById)}Feature: {statusCode}";
    }
}
