namespace BookShop.Application.Features.Orders.RemoveOrderTemporarilyById;

/// <summary>
///     Extension Method for RemoveOrderTemporarilyById features.
/// </summary>
public static class RemoveOrderTemporarilyByIdExtensionMethod
{
    public static string ToAppCode(this RemoveOrderTemporarilyByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveOrderTemporarilyById)}Feature: {statusCode}";
    }
}
