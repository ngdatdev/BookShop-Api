namespace BookShop.API.Controllers.Address.GetAllDistrictsByProvinceName.HttpResponseMapper;

/// <summary>
///     GetAllDistrictsByProvinceName extension method
/// </summary>
internal static class GetAllDistrictsByProvinceNameHttpResponseMapper
{
    private static GetAllDistrictsByProvinceNameHttpResponseManager _GetAllDistrictsByProvinceNameHttpResponseManager;

    internal static GetAllDistrictsByProvinceNameHttpResponseManager Get()
    {
        return _GetAllDistrictsByProvinceNameHttpResponseManager ??= new();
    }
}
