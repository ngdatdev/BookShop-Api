namespace BookShop.Application.Features.OrderDetails.GetOrderDetailsByOrderStatusId;

/// <summary>
///     Extension Method for GetOrderDetailsByOrderStatusId features.
/// </summary>
public static class GetOrderDetailsByOrderStatusIdExtensionMethod
{
    public static string ToAppCode(this GetOrderDetailsByOrderStatusIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetOrderDetailsByOrderStatusId)}Feature: {statusCode}";
    }
}
