namespace BookShop.API.Controllers.Order.GetAllOrders.HttpResponseMapper;

/// <summary>
///     GetAllOrders extension method
/// </summary>
internal static class GetAllOrdersHttpResponseMapper
{
    private static GetAllOrdersHttpResponseManager _GetAllOrdersHttpResponseManager;

    internal static GetAllOrdersHttpResponseManager Get()
    {
        return _GetAllOrdersHttpResponseManager ??= new();
    }
}
