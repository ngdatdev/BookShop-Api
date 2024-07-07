namespace BookShop.Application.Features.Reviews.RemoveReviewById;

/// <summary>
///     Extension Method for RemoveReviewById features.
/// </summary>
public static class RemoveReviewByIdExtensionMethod
{
    public static string ToAppCode(this RemoveReviewByIdResponseStatusCode statusCode)
    {
        return $"{nameof(RemoveReviewById)}Feature: {statusCode}";
    }
}
