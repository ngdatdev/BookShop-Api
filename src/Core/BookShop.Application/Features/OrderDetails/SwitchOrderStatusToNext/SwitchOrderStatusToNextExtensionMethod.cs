namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///     Extension Method for SwitchOrderStatusToNext features.
/// </summary>
public static class SwitchOrderStatusToNextExtensionMethod
{
    public static string ToAppCode(
        this SwitchOrderStatusToNextResponseStatusCode statusCode
    )
    {
        return $"{nameof(SwitchOrderStatusToNext)}Feature: {statusCode}";
    }
}
