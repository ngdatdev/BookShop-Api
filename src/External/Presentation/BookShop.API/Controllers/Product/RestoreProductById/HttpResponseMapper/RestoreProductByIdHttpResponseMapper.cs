namespace BookShop.API.Controllers.Product.RestoreProductById.HttpResponseMapper;

/// <summary>
///     RestoreProductById extension method
/// </summary>
internal static class RestoreProductByIdHttpResponseMapper
{
    private static RestoreProductByIdHttpResponseManager _RestoreProductByIddHttpResponseManager;

    internal static RestoreProductByIdHttpResponseManager Get()
    {
        return _RestoreProductByIddHttpResponseManager ??= new();
    }
}
