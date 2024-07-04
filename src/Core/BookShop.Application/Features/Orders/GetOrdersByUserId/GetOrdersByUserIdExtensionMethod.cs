namespace BookShop.Application.Features.Orders.GetOrdersByUserId;

/// <summary>
///     Extension Method for GetOrdersByUserId features.
/// </summary>
public static class GetOrdersByUserIdExtensionMethod
{
    public static string ToAppCode(this GetOrdersByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetOrdersByUserId)}Feature: {statusCode}";
    }
}
