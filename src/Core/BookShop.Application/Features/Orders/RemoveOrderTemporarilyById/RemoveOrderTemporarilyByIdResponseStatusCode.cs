namespace BookShop.Application.Features.Orders.RemoveOrderTemporarilyById;

/// <summary>
///     RemoveOrderTemporarilyById Response Status Code
/// </summary>
public enum RemoveOrderTemporarilyByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    ORDER_IS_ALREADY_TEMPORARILY_REMOVED,
    ORDER_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
