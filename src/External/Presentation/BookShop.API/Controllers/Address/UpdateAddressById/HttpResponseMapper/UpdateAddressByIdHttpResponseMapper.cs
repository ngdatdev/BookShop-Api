namespace BookShop.API.Controllers.Address.UpdateAddressById.HttpResponseMapper;

/// <summary>
///     UpdateAddressById extension method
/// </summary>
internal static class UpdateAddressByIdHttpResponseMapper
{
    private static UpdateAddressByIdHttpResponseManager _UpdateAddressByIdHttpResponseManager;

    internal static UpdateAddressByIdHttpResponseManager Get()
    {
        return _UpdateAddressByIdHttpResponseManager ??= new();
    }
}
