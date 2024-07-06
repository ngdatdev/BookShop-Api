namespace BookShop.API.Controllers.Address.GetAddressesByWard.HttpResponseMapper;

/// <summary>
///     GetAddressesByWard extension method
/// </summary>
internal static class GetAddressesByWardHttpResponseMapper
{
    private static GetAddressesByWardHttpResponseManager _GetAddressesByWardHttpResponseManager;

    internal static GetAddressesByWardHttpResponseManager Get()
    {
        return _GetAddressesByWardHttpResponseManager ??= new();
    }
}
