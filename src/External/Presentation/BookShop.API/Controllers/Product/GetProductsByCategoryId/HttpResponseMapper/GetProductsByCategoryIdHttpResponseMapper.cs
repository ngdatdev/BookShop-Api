namespace BookShop.API.Controllers.Product.GetProductsByCategoryId.HttpResponseMapper;

/// <summary>
///     GetProductsByCategoryId extension method
/// </summary>
internal static class GetProductsByCategoryIdHttpResponseMapper
{
    private static GetProductsByCategoryIdHttpResponseManager _GetProductsByCategoryIdHttpResponseManager;

    internal static GetProductsByCategoryIdHttpResponseManager Get()
    {
        return _GetProductsByCategoryIdHttpResponseManager ??= new();
    }
}
