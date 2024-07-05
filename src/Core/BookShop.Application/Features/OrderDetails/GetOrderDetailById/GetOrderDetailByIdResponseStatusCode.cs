namespace BookShop.Application.Features.OrderDetails.GetOrderDetailById;

/// <summary>
///     GetOrderDetailById Response Status Code
/// </summary>
public enum GetOrderDetailByIdResponseStatusCode
{
    ORDER_DETAIL_IS_NOT_FOUND,
    ORDER_DETAIL_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
