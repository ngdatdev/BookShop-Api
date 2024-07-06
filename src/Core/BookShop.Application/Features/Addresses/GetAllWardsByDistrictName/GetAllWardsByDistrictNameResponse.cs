using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.GetAllWardsByDistrictName;

/// <summary>
///     GetAllWardsByDistrictName Response
/// </summary>
public class GetAllWardsByDistrictNameResponse : IFeatureResponse
{
    public GetAllWardsByDistrictNameResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<string> Wards { get; init; }
    }
}
