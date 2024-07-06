namespace BookShop.Application.Features.OrderDetails.RestoreOrderDetailById;

/// <summary>
///     Extension Method for RestoreOrderDetailById features.
/// </summary>
public static class RestoreOrderDetailByIdExtensionMethod
{
    public static string ToAppCode(this RestoreOrderDetailByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RestoreOrderDetailById)}Feature: {statusCode}";
    }
}
