namespace BookShop.Application.Features.Product.GetProductsByAuthorName;

/// <summary>
///     Extension Method for GetProductsByAuthorName features.
/// </summary>
public static class GetProductsByAuthorNameExtensionMethod
{
    public static string ToAppCode(this GetProductsByAuthorNameResponseStatusCode statusCode)
    {
        return $"{nameof(GetProductsByAuthorName)}Feature: {statusCode}";
    }
}
