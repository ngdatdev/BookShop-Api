using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.GetAllWardsByDistrictName;

/// <summary>
///     GetAllWardsByDistrictName Request
/// </summary>
public class GetAllWardsByDistrictNameRequest
    : IFeatureRequest<GetAllWardsByDistrictNameResponse>
{
    public string District { get; set; }
}
