namespace BookShop.Application.Features.Product.RemoveProductTemporarilyById;

/// <summary>
///     RemoveProductTemporarilyById Response Status Code
/// </summary>
public enum RemoveProductTemporarilyByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    PRODUCT_IS_ALREADY_TEMPORARILY_REMOVED,
    PRODUCT_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
