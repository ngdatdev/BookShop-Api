namespace BookShop.API.Controllers.Cart.GetCartByUserId.HttpResponseMapper;

/// <summary>
///     GetCartByUserId extension method
/// </summary>
internal static class GetCartByUserIdHttpResponseMapper
{
    private static GetCartByUserIdHttpResponseManager _GetCartByUserIdHttpResponseManager;

    internal static GetCartByUserIdHttpResponseManager Get()
    {
        return _GetCartByUserIdHttpResponseManager ??= new();
    }
}
