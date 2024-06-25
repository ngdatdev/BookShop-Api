namespace BookShop.API.Controllers.Product.GetProductById.HttpResponseMapper;

/// <summary>
///     GetProductById extension method
/// </summary>
internal static class GetProductByIdHttpResponseMapper
{
    private static GetProductByIdHttpResponseManager _GetProductByIdHttpResponseManager;

    internal static GetProductByIdHttpResponseManager Get()
    {
        return _GetProductByIdHttpResponseManager ??= new();
    }
}
