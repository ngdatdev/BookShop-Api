namespace BookShop.API.Controllers.OrderDetail.RestoreOrderStatusToConfirm.HttpResponseMapper;

/// <summary>
///     RestoreOrderStatusToConfirm extension method
/// </summary>
internal static class RestoreOrderStatusToConfirmHttpResponseMapper
{
    private static RestoreOrderStatusToConfirmHttpResponseManager _RestoreOrderStatusToConfirmHttpResponseManager;

    internal static RestoreOrderStatusToConfirmHttpResponseManager Get()
    {
        return _RestoreOrderStatusToConfirmHttpResponseManager ??= new();
    }
}
