namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToNext.HttpResponseMapper;

/// <summary>
///     SwitchOrderStatusToNext extension method
/// </summary>
internal static class SwitchOrderStatusToNextHttpResponseMapper
{
    private static SwitchOrderStatusToNextHttpResponseManager _SwitchOrderStatusToNextHttpResponseManager;

    internal static SwitchOrderStatusToNextHttpResponseManager Get()
    {
        return _SwitchOrderStatusToNextHttpResponseManager ??= new();
    }
}
