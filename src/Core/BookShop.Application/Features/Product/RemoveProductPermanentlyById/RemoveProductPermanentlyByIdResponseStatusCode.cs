namespace BookShop.Application.Features.Product.RemoveProductPermanentlyById;

/// <summary>
///     RemoveProductPermanentlyById Response Status Code
/// </summary>
public enum RemoveProductPermanentlyByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    PRODUCT_IS_NOT_TEMPORARILY_REMOVED,
    PRODUCT_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
