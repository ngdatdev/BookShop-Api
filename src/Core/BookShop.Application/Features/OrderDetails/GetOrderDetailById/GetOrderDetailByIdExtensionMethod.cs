namespace BookShop.Application.Features.OrderDetails.GetOrderDetailById;

/// <summary>
///     Extension Method for GetOrderDetailById features.
/// </summary>
public static class GetOrderDetailByIdExtensionMethod
{
    public static string ToAppCode(this GetOrderDetailByIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetOrderDetailById)}Feature: {statusCode}";
    }
}
