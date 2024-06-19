namespace BookShop.API.Controllers.Product.GetAllProductsEndpoint.HttpResponseMapper;

/// <summary>
///     GetAllProducts extension method
/// </summary>
internal static class GetAllProductsHttpResponseMapper
{
    private static GetAllProductsHttpResponseManager _GetAllProductsHttpResponseManager;

    internal static GetAllProductsHttpResponseManager Get()
    {
        return _GetAllProductsHttpResponseManager ??= new();
    }
}
