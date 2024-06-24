namespace BookShop.Application.Features.Product.GetProductById;

/// <summary>
///     GetProductById Response Status Code
/// </summary>
public enum GetProductByIdResponseStatusCode
{
    PRODUCT_ID_IS_NOT_FOUND,
    PRODUCT_IS_TEMPORARILY_REMOVED,
    OPERATION_SUCCESS,
}
