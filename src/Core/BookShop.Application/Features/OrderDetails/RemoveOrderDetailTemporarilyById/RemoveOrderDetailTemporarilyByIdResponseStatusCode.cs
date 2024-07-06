namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///     RemoveOrderDetailTemporarilyById Response Status Code
/// </summary>
public enum RemoveOrderDetailTemporarilyByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED,
    ORDER_DETAIL_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
