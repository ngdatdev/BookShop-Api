namespace BookShop.Application.Features.Addresses.GetAllWardsByDistrictName;

/// <summary>
///     Extension Method for GetAllWardsByDistrictName features.
/// </summary>
public static class GetAllWardsByDistrictNameExtensionMethod
{
    public static string ToAppCode(this GetAllWardsByDistrictNameResponseStatusCode statusCode)
    {
        return $"{nameof(GetAllWardsByDistrictName)}Feature: {statusCode}";
    }
}
