namespace BookShop.Application.Features.Orders.RemoveOrderPermanentlyById;

/// <summary>
///     RemoveOrderPermanentlyById Response Status Code
/// </summary>
public enum RemoveOrderPermanentlyByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    ORDER_IS_NOT_TEMPORARILY_REMOVED,
    ORDER_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
