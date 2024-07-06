namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///     Extension Method for RemoveOrderDetailPermanentlyById features.
/// </summary>
public static class RemoveOrderDetailPermanentlyByIdExtensionMethod
{
    public static string ToAppCode(
        this RemoveOrderDetailPermanentlyByIdResponseStatusCode statusCode
    )
    {
        return $"{nameof(RemoveOrderDetailPermanentlyById)}Feature: {statusCode}";
    }
}
