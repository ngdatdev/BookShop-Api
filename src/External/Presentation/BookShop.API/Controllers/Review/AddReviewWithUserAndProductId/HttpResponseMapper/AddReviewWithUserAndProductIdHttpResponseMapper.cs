namespace BookShop.API.Controllers.Review.AddReviewWithUserAndProductId.HttpResponseMapper;

/// <summary>
///     AddReviewWithUserAndProductId extension method
/// </summary>
internal static class AddReviewWithUserAndProductIdHttpResponseMapper
{
    private static AddReviewWithUserAndProductIdHttpResponseManager _AddReviewWithUserAndProductIdHttpResponseManager;

    internal static AddReviewWithUserAndProductIdHttpResponseManager Get()
    {
        return _AddReviewWithUserAndProductIdHttpResponseManager ??= new();
    }
}
