namespace BookShop.API.Controllers.Product.UpdateProductByIdEndpoint.HttpResponseMapper;

/// <summary>
///     UpdateProductById extension method
/// </summary>
internal static class UpdateProductByIdHttpResponseMapper
{
    private static UpdateProductByIdHttpResponseManager _UpdateProductByIdHttpResponseManager;

    internal static UpdateProductByIdHttpResponseManager Get()
    {
        return _UpdateProductByIdHttpResponseManager ??= new();
    }
}
