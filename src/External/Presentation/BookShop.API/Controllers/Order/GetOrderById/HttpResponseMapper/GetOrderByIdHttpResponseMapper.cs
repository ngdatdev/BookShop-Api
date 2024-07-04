namespace BookShop.API.Controllers.Order.GetOrderById.HttpResponseMapper;

/// <summary>
///     GetOrderById extension method
/// </summary>
internal static class GetOrderByIdHttpResponseMapper
{
    private static GetOrderByIdHttpResponseManager _GetOrderByIdHttpResponseManager;

    internal static GetOrderByIdHttpResponseManager Get()
    {
        return _GetOrderByIdHttpResponseManager ??= new();
    }
}
