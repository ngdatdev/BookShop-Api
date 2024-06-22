namespace BookShop.Application.Features.Product.GetProductsByCategoryId;

/// <summary>
///     Extension Method for GetProductsByCategoryId features.
/// </summary>
public static class GetProductsByCategoryIdExtensionMethod
{
    public static string ToAppCode(this GetProductsByCategoryIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetProductsByCategoryId)}Feature: {statusCode}";
    }
}
