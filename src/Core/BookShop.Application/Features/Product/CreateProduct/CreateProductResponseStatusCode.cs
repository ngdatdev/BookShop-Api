namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     CreateProduct Response Status Code
/// </summary>
public enum CreateProductResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    INPUT_VALIDATION_FAIL,
    MAIN_IMAGE_FILE_FAIL,
    SUB_IMAGE_FILE_FAIL,
    CATEGORY_ID_IS_NOT_VALID
}
