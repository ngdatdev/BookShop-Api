namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///     Extension Method for RemoveOrderDetailTemporarilyById features.
/// </summary>
public static class RemoveOrderDetailTemporarilyByIdExtensionMethod
{
    public static string ToAppCode(
        this RemoveOrderDetailTemporarilyByIdResponseStatusCode statusCode
    )
    {
        return $"{nameof(RemoveOrderDetailTemporarilyById)}Feature: {statusCode}";
    }
}
