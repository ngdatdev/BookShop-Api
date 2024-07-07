namespace BookShop.API.Controllers.Review.GetReviewsByUserId.HttpResponseMapper;

/// <summary>
///     GetReviewsByUserId extension method
/// </summary>
internal static class GetReviewsByUserIdHttpResponseMapper
{
    private static GetReviewsByUserIdHttpResponseManager _GetReviewsByUserIdHttpResponseManager;

    internal static GetReviewsByUserIdHttpResponseManager Get()
    {
        return _GetReviewsByUserIdHttpResponseManager ??= new();
    }
}
