namespace BookShop.API.Controllers.Product.GetProductsByAuthorNameEndpoint.HttpResponseMapper;

/// <summary>
///     GetProductsByAuthorName extension method
/// </summary>
internal static class GetProductsByAuthorNameHttpResponseMapper
{
    private static GetProductsByAuthorNameHttpResponseManager _GetProductsByAuthorNameHttpResponseManager;

    internal static GetProductsByAuthorNameHttpResponseManager Get()
    {
        return _GetProductsByAuthorNameHttpResponseManager ??= new();
    }
}
