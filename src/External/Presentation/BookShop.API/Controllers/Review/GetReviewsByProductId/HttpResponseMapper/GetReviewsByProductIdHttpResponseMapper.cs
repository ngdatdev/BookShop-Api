namespace BookShop.API.Controllers.Review.GetReviewsByProductId.HttpResponseMapper;

/// <summary>
///     GetReviewsByProductId extension method
/// </summary>
internal static class GetReviewsByProductIdHttpResponseMapper
{
    private static GetReviewsByProductIdHttpResponseManager _GetReviewsByProductIdHttpResponseManager;

    internal static GetReviewsByProductIdHttpResponseManager Get()
    {
        return _GetReviewsByProductIdHttpResponseManager ??= new();
    }
}
