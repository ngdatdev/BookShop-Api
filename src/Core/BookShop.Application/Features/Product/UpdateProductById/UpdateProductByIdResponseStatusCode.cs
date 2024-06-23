namespace BookShop.Application.Features.Product.UpdateProductById;

/// <summary>
///     UpdateProductById Response Status Code
/// </summary>
public enum UpdateProductByIdResponseStatusCode
{
    CATEGORY_ID_IS_NOT_FOUND,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MAIN_IMAGE_FILE_FAIL,
    SUB_IMAGE_FILE_FAIL,
    CATEGORY_ID_IS_NOT_VALID,
    PRODUCT_ID_IS_NOT_FOUND
}
