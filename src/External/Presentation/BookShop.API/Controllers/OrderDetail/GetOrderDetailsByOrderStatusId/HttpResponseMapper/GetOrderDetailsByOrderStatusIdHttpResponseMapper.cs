namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.HttpResponseMapper;

/// <summary>
///     GetOrderDetailsByOrderStatusId extension method
/// </summary>
internal static class GetOrderDetailsByOrderStatusIdHttpResponseMapper
{
    private static GetOrderDetailsByOrderStatusIdHttpResponseManager _GetOrderDetailsByOrderStatusIdHttpResponseManager;

    internal static GetOrderDetailsByOrderStatusIdHttpResponseManager Get()
    {
        return _GetOrderDetailsByOrderStatusIdHttpResponseManager ??= new();
    }
}
