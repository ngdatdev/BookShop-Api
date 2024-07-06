namespace BookShop.API.Controllers.Address.GetAllWardsByDistrictName.HttpResponseMapper;

/// <summary>
///     GetAllWardsByDistrictName extension method
/// </summary>
internal static class GetAllWardsByDistrictNameHttpResponseMapper
{
    private static GetAllWardsByDistrictNameHttpResponseManager _GetAllWardsByDistrictNameHttpResponseManager;

    internal static GetAllWardsByDistrictNameHttpResponseManager Get()
    {
        return _GetAllWardsByDistrictNameHttpResponseManager ??= new();
    }
}
