namespace BookShop.Application.Features.Orders.GetOrderById;

/// <summary>
///     Extension Method for GetOrderById features.
/// </summary>
public static class GetOrderByIdExtensionMethod
{
    public static string ToAppCode(this GetOrderByIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetOrderById)}Feature: {statusCode}";
    }
}
