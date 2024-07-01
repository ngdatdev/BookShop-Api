namespace BookShop.API.Controllers.CartItem.UpdateCartItemById.HttpResponseMapper;

/// <summary>
///     UpdateCartItemById extension method
/// </summary>
internal static class UpdateCartItemByIdHttpResponseMapper
{
    private static UpdateCartItemByIdHttpResponseManager _UpdateCartItemByIdHttpResponseManager;

    internal static UpdateCartItemByIdHttpResponseManager Get()
    {
        return _UpdateCartItemByIdHttpResponseManager ??= new();
    }
}
