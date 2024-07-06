using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.GetAllDistrictsByProvinceName;

/// <summary>
///     GetAllDistrictsByProvinceName Request
/// </summary>
public class GetAllDistrictsByProvinceNameRequest
    : IFeatureRequest<GetAllDistrictsByProvinceNameResponse>
{
    public string Province { get; set; }
}
