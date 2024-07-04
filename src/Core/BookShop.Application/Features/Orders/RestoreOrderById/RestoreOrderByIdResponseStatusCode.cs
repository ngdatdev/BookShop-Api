namespace BookShop.Application.Features.Orders.RestoreOrderById;

/// <summary>
///     RestoreOrderById Response Status Code
/// </summary>
public enum RestoreOrderByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    ORDER_IS_NOT_TEMPORARILY_REMOVED,
    ORDER_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
