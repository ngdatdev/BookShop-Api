namespace BookShop.Application.Features.Product.GetProductsByCategoryId;

/// <summary>
///     GetProductsByCategoryId Response Status Code
/// </summary>
public enum GetProductsByCategoryIdResponseStatusCode
{
    CATEGORY_ID_IS_NOT_CORRECT,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
}
