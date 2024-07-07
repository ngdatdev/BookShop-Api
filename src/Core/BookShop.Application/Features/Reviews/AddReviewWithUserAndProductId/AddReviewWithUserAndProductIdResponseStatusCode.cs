namespace BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///     AddReviewWithUserAndProductId Response Status Code
/// </summary>
public enum AddReviewWithUserAndProductIdResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    PRODUCT_IS_NOT_FOUND,
    PRODUCT_IS_TEMPORARILY_REMOVED,
}
