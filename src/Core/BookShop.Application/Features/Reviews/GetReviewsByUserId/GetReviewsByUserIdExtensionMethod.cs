namespace BookShop.Application.Features.Reviews.GetReviewsByUserId;

/// <summary>
///     Extension Method for GetReviewsByUserId features.
/// </summary>
public static class GetReviewsByUserIdExtensionMethod
{
    public static string ToAppCode(this GetReviewsByUserIdResponseStatusCode statusCode)
    {
        return $"{nameof(GetReviewsByUserId)}Feature: {statusCode}";
    }
}
