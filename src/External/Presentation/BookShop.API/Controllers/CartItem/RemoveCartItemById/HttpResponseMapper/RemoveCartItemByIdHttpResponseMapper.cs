namespace BookShop.API.Controllers.CartItem.RemoveCartItemById.HttpResponseMapper;

/// <summary>
///     RemoveCartItemById extension method
/// </summary>
internal static class RemoveCartItemByIdHttpResponseMapper
{
    private static RemoveCartItemByIdHttpResponseManager _RemoveCartItemByIdHttpResponseManager;

    internal static RemoveCartItemByIdHttpResponseManager Get()
    {
        return _RemoveCartItemByIdHttpResponseManager ??= new();
    }
}
