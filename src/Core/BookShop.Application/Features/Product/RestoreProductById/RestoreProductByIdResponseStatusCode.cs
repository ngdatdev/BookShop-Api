namespace BookShop.Application.Features.Product.RestoreProductById;

/// <summary>
///     RestoreProductById Response Status Code
/// </summary>
public enum RestoreProductByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    PRODUCT_IS_ALREADY_TEMPORARILY_REMOVED,
    PRODUCT_ID_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
