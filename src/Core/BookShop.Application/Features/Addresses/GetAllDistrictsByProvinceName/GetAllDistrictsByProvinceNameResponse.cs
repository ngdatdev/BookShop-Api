using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.GetAllDistrictsByProvinceName;

/// <summary>
///     GetAllDistrictsByProvinceName Response
/// </summary>
public class GetAllDistrictsByProvinceNameResponse : IFeatureResponse
{
    public GetAllDistrictsByProvinceNameResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<string> Districts { get; init; }
    }
}
