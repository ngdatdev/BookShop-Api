namespace BookShop.API.Controllers.Review.RemoveReviewById.HttpResponseMapper;

/// <summary>
///     RemoveReviewById extension method
/// </summary>
internal static class RemoveReviewByIdHttpResponseMapper
{
    private static RemoveReviewByIdHttpResponseManager _RemoveReviewByIdHttpResponseManager;

    internal static RemoveReviewByIdHttpResponseManager Get()
    {
        return _RemoveReviewByIdHttpResponseManager ??= new();
    }
}
