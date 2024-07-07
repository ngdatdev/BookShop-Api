namespace BookShop.Application.Features.Reviews.GetReviewsByProductId;

/// <summary>
///     Extension Method for GetReviewsByProductId features.
/// </summary>
public static class GetReviewsByProductIdExtensionMethod
{
    public static string ToAppCode(this GetReviewsByProductIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetReviewsByProductId)}Feature: {statusCode}";
    }
}
