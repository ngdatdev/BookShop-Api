namespace BookShop.Application.Features.Orders.RestoreOrderById;

/// <summary>
///     Extension Method for RestoreOrderById features.
/// </summary>
public static class RestoreOrderByIdExtensionMethod
{
    public static string ToAppCode(this RestoreOrderByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RestoreOrderById)}Feature: {statusCode}";
    }
}
