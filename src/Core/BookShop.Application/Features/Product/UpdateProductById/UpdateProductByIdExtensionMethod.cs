namespace BookShop.Application.Features.Product.UpdateProductById;

/// <summary>
///     Extension Method for UpdateProductById features.
/// </summary>
public static class UpdateProductByIdExtensionMethod
{
    public static string ToAppCode(this UpdateProductByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateProductById)}Feature: {statusCode}";
    }
}
