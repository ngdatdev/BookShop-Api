namespace BookShop.API.Controllers.Review.UpdateReviewById.HttpResponseMapper;

/// <summary>
///     UpdateReviewById extension method
/// </summary>
internal static class UpdateReviewByIdHttpResponseMapper
{
    private static UpdateReviewByIdHttpResponseManager _UpdateReviewByIdHttpResponseManager;

    internal static UpdateReviewByIdHttpResponseManager Get()
    {
        return _UpdateReviewByIdHttpResponseManager ??= new();
    }
}
