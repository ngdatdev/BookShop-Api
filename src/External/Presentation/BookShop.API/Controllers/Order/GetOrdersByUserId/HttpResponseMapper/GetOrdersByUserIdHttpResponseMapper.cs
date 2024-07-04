namespace BookShop.API.Controllers.Order.GetOrdersByUserId.HttpResponseMapper;

/// <summary>
///     GetOrdersByUserId extension method
/// </summary>
internal static class GetOrdersByUserIdHttpResponseMapper
{
    private static GetOrdersByUserIdHttpResponseManager _GetOrdersByUserIdHttpResponseManager;

    internal static GetOrdersByUserIdHttpResponseManager Get()
    {
        return _GetOrdersByUserIdHttpResponseManager ??= new();
    }
}
