namespace BookShop.Application.Features.Reviews.UpdateReviewById;

/// <summary>
///     Extension Method for UpdateReviewById features.
/// </summary>
public static class UpdateReviewByIdExtensionMethod
{
    public static string ToAppCode(this UpdateReviewByIdResponseStatusCode statusCode)
    {
        return $"{nameof(UpdateReviewById)}Feature: {statusCode}";
    }
}
