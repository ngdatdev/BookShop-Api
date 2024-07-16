namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///     SwitchOrderStatusToCancel Response Status Code
/// </summary>
public enum SwitchOrderStatusToCancelResponseStatusCode
{
    OPERATION_SUCCESS,
    ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED,
    ORDER_DETAIL_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
    ORDER_STATUS_IS_CAN_NOT_CANCEL
}
