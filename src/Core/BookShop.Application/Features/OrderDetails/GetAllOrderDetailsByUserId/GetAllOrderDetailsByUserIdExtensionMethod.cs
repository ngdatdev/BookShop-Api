namespace BookShop.Application.Features.OrderDetails.GetAllOrderDetailsByUserId;

/// <summary>
///     Extension Method for GetAllOrderDetailsByUserId features.
/// </summary>
public static class GetAllOrderDetailsByUserIdExtensionMethod
{
    public static string ToAppCode(this GetAllOrderDetailsByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllOrderDetailsByUserId)}Feature: {statusCode}";
    }
}
