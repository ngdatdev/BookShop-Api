namespace BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///     Extension Method for RestoreOrderStatusToConfirm features.
/// </summary>
public static class RestoreOrderStatusToConfirmExtensionMethod
{
    public static string ToAppCode(this RestoreOrderStatusToConfirmResponseStatusCode statusCode)
    {
        return $"{nameof(RestoreOrderStatusToConfirm)}Feature: {statusCode}";
    }
}
