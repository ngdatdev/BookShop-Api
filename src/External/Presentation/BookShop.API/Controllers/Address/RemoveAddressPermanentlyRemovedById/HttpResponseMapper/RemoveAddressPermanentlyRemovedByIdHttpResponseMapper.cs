namespace BookShop.API.Controllers.Address.RemoveAddressPermanentlyRemovedById.HttpResponseMapper;

/// <summary>
///     RemoveAddressPermanentlyRemovedById extension method
/// </summary>
internal static class RemoveAddressPermanentlyRemovedByIdHttpResponseMapper
{
    private static RemoveAddressPermanentlyRemovedByIdHttpResponseManager _RemoveAddressPermanentlyRemovedByIdHttpResponseManager;

    internal static RemoveAddressPermanentlyRemovedByIdHttpResponseManager Get()
    {
        return _RemoveAddressPermanentlyRemovedByIdHttpResponseManager ??= new();
    }
}
