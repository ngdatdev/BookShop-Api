namespace BookShop.API.Controllers.Product.SearchProductsByKeyword.HttpResponseMapper;

/// <summary>
///     SearchProductsByKeyword extension method
/// </summary>
internal static class SearchProductsByKeywordHttpResponseMapper
{
    private static SearchProductsByKeywordHttpResponseManager _SearchProductsByKeywordHttpResponseManager;

    internal static SearchProductsByKeywordHttpResponseManager Get()
    {
        return _SearchProductsByKeywordHttpResponseManager ??= new();
    }
}
