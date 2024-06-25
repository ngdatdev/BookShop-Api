namespace BookShop.API.Controllers.Product.GetAllProducts.HttpResponseMapper;

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
