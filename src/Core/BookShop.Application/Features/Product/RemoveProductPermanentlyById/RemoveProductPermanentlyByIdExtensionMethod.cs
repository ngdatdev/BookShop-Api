namespace BookShop.Application.Features.Product.RemoveProductPermanentlyById;

/// <summary>
///     Extension Method for RemoveProductPermanentlyById features.
/// </summary>
public static class RemoveProductPermanentlyByIdExtensionMethod
{
    public static string ToAppCode(this RemoveProductPermanentlyByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveProductPermanentlyById)}Feature: {statusCode}";
    }
}
