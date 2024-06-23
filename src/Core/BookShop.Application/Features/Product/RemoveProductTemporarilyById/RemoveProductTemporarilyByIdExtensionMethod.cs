namespace BookShop.Application.Features.Product.RemoveProductTemporarilyById;

/// <summary>
///     Extension Method for RemoveProductTemporarilyById features.
/// </summary>
public static class RemoveProductTemporarilyByIdExtensionMethod
{
    public static string ToAppCode(this RemoveProductTemporarilyByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveProductTemporarilyById)}Feature: {statusCode}";
    }
}
