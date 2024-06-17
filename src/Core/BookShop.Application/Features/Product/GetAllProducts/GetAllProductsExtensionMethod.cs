namespace BookShop.Application.Features.Product.GetAllProducts;

/// <summary>
///     Extension Method for GetAllProducts features.
/// </summary>
public static class GetAllProductsExtensionMethod
{
    public static string ToAppCode(this GetAllProductsResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllProducts)}Feature: {statusCode}";
    }
}
