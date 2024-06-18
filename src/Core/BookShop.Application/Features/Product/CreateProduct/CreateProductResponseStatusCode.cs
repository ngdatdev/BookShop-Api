namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     CreateProduct Response Status Code
/// </summary>
public enum CreateProductResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    INPUT_VALIDATION_FAIL,
    IMAGE_FILE_FAIL
}
