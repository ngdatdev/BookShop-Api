namespace BookShop.Application.Features.OrderDetails.RestoreOrderDetailById;

/// <summary>
///     RestoreOrderDetailById Response Status Code
/// </summary>
public enum RestoreOrderDetailByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    ORDER_DETAIL_IS_NOT_TEMPORARILY_REMOVED,
    ORDER_DETAIL_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
