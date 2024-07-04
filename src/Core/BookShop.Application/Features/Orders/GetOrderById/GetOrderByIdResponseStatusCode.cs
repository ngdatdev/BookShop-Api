namespace BookShop.Application.Features.Orders.GetOrderById;

/// <summary>
///     GetOrderById Response Status Code
/// </summary>
public enum GetOrderByIdResponseStatusCode
{
    ORDER_ID_IS_NOT_FOUND,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
