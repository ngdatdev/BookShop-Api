namespace BookShop.Application.Features.Orders.GetAllOrders;

/// <summary>
///     Extension Method for GetAllOrders features.
/// </summary>
public static class GetAllOrdersExtensionMethod
{
    public static string ToAppCode(this GetAllOrdersResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllOrders)}Feature: {statusCode}";
    }
}
