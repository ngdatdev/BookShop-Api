namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///     SwitchOrderStatusToNext Response Status Code
/// </summary>
public enum SwitchOrderStatusToNextResponseStatusCode
{
    OPERATION_SUCCESS,
    ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED,
    ORDER_DETAIL_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
    ORDER_STATUS_IS_END
}
