namespace BookShop.API.Controllers.Cart.ClearCart.HttpResponseMapper;

/// <summary>
///     ClearCart extension method
/// </summary>
internal static class ClearCartHttpResponseMapper
{
    private static ClearCartHttpResponseManager _ClearCartHttpResponseManager;

    internal static ClearCartHttpResponseManager Get()
    {
        return _ClearCartHttpResponseManager ??= new();
    }
}
