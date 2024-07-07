namespace BookShop.API.Controllers.Address.RestoreAddressById.HttpResponseMapper;

/// <summary>
///     RestoreAddressById extension method
/// </summary>
internal static class RestoreAddressByIdHttpResponseMapper
{
    private static RestoreAddressByIdHttpResponseManager _RestoreAddressByIdHttpResponseManager;

    internal static RestoreAddressByIdHttpResponseManager Get()
    {
        return _RestoreAddressByIdHttpResponseManager ??= new();
    }
}
