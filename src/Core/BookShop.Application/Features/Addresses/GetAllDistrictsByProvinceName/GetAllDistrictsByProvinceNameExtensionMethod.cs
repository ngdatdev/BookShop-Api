namespace BookShop.Application.Features.Addresses.GetAllDistrictsByProvinceName;

/// <summary>
///     Extension Method for GetAllDistrictsByProvinceName features.
/// </summary>
public static class GetAllDistrictsByProvinceNameExtensionMethod
{
    public static string ToAppCode(this GetAllDistrictsByProvinceNameResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllDistrictsByProvinceName)}Feature: {statusCode}";
    }
}
