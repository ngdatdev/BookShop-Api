namespace BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.HttpResponseMapper;

/// <summary>
///     GetAllOrderDetailsByUserId extension method
/// </summary>
internal static class GetAllOrderDetailsByUserIdHttpResponseMapper
{
    private static GetAllOrderDetailsByUserIdHttpResponseManager _GetAllOrderDetailsByUserIdHttpResponseManager;

    internal static GetAllOrderDetailsByUserIdHttpResponseManager Get()
    {
        return _GetAllOrderDetailsByUserIdHttpResponseManager ??= new();
    }
}
