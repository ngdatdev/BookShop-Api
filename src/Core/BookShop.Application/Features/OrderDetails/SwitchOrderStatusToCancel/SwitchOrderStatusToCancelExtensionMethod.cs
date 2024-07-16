namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///     Extension Method for SwitchOrderStatusToCancel features.
/// </summary>
public static class SwitchOrderStatusToCancelExtensionMethod
{
    public static string ToAppCode(this SwitchOrderStatusToCancelResponseStatusCode statusCode)
    {
        return $"{nameof(SwitchOrderStatusToCancel)}Feature: {statusCode}";
    }
}
