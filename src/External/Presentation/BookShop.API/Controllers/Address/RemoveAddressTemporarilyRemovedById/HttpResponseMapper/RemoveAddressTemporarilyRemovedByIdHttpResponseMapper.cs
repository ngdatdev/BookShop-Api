namespace BookShop.API.Controllers.Address.RemoveAddressTemporarilyRemovedById.HttpResponseMapper;

/// <summary>
///     RemoveAddressTemporarilyRemovedById extension method
/// </summary>
internal static class RemoveAddressTemporarilyRemovedByIdHttpResponseMapper
{
    private static RemoveAddressTemporarilyRemovedByIdHttpResponseManager _RemoveAddressTemporarilyRemovedByIdHttpResponseManager;

    internal static RemoveAddressTemporarilyRemovedByIdHttpResponseManager Get()
    {
        return _RemoveAddressTemporarilyRemovedByIdHttpResponseManager ??= new();
    }
}
