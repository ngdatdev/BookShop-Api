namespace BookShop.API.Controllers.CartItem.AddItemToCart.HttpResponseMapper;

/// <summary>
///     AddItemToCart extension method
/// </summary>
internal static class AddItemToCartHttpResponseMapper
{
    private static AddItemToCartHttpResponseManager _AddItemToCartHttpResponseManager;

    internal static AddItemToCartHttpResponseManager Get()
    {
        return _AddItemToCartHttpResponseManager ??= new();
    }
}
