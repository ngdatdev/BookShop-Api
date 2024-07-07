namespace BookShop.API.Controllers.Address.GetAllTemporarilyAddresses.HttpResponseMapper;

/// <summary>
///     GetAllTemporarilyAddresses extension method
/// </summary>
internal static class GetAllTemporarilyAddressesHttpResponseMapper
{
    private static GetAllTemporarilyAddressesHttpResponseManager _GetAllTemporarilyAddressesHttpResponseManager;

    internal static GetAllTemporarilyAddressesHttpResponseManager Get()
    {
        return _GetAllTemporarilyAddressesHttpResponseManager ??= new();
    }
}
