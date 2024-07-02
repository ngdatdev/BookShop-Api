namespace BookShop.API.Controllers.Order.CreateOrder.HttpResponseMapper;

/// <summary>
///     CreateOrder extension method
/// </summary>
internal static class CreateOrderHttpResponseMapper
{
    private static CreateOrderHttpResponseManager _CreateOrderHttpResponseManager;

    internal static CreateOrderHttpResponseManager Get()
    {
        return _CreateOrderHttpResponseManager ??= new();
    }
}
