namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToCancel.HttpResponseMapper;

/// <summary>
///     SwitchOrderStatusToCancel extension method
/// </summary>
internal static class SwitchOrderStatusToCancelHttpResponseMapper
{
    private static SwitchOrderStatusToCancelHttpResponseManager _SwitchOrderStatusToCancelHttpResponseManager;

    internal static SwitchOrderStatusToCancelHttpResponseManager Get()
    {
        return _SwitchOrderStatusToCancelHttpResponseManager ??= new();
    }
}
