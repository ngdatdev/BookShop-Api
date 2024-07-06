namespace BookShop.API.Controllers.OrderDetail.RestoreOrderDetailById.HttpResponseMapper;

/// <summary>
///     RestoreOrderDetailById extension method
/// </summary>
internal static class RestoreOrderDetailByIdHttpResponseMapper
{
    private static RestoreOrderDetailByIdHttpResponseManager _RestoreOrderDetailByIdHttpResponseManager;

    internal static RestoreOrderDetailByIdHttpResponseManager Get()
    {
        return _RestoreOrderDetailByIdHttpResponseManager ??= new();
    }
}
