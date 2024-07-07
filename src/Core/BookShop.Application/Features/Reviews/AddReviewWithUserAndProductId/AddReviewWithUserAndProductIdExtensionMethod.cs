namespace BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///     Extension Method for AddReviewWithUserAndProductId features.
/// </summary>
public static class AddReviewWithUserAndProductIdExtensionMethod
{
    public static string ToAppCode(this AddReviewWithUserAndProductIdResponseStatusCode statusCode)
    {
        return $"{nameof(AddReviewWithUserAndProductId)}Feature: {statusCode}";
    }
}
