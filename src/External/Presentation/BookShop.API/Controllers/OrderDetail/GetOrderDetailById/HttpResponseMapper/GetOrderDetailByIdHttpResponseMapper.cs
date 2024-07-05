namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailById.HttpResponseMapper;

/// <summary>
///     GetOrderDetailById extension method
/// </summary>
internal static class GetOrderDetailByIdHttpResponseMapper
{
    private static GetOrderDetailByIdHttpResponseManager _GetOrderDetailByIdHttpResponseManager;

    internal static GetOrderDetailByIdHttpResponseManager Get()
    {
        return _GetOrderDetailByIdHttpResponseManager ??= new();
    }
}
