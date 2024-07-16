namespace BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///     RestoreOrderStatusToConfirm Response Status Code
/// </summary>
public enum RestoreOrderStatusToConfirmResponseStatusCode
{
    OPERATION_SUCCESS,
    ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED,
    ORDER_DETAIL_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
    ORDER_STATUS_IS_NOT_CANCELED
}
