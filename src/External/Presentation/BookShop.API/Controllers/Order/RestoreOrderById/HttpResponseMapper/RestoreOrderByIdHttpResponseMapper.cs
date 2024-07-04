namespace BookShop.API.Controllers.Order.RestoreOrderById.HttpResponseMapper;

/// <summary>
///     RestoreOrderById extension method
/// </summary>
internal static class RestoreOrderByIdHttpResponseMapper
{
    private static RestoreOrderByIdHttpResponseManager _RestoreOrderByIdHttpResponseManager;

    internal static RestoreOrderByIdHttpResponseManager Get()
    {
        return _RestoreOrderByIdHttpResponseManager ??= new();
    }
}
