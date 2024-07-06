using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.GetAddressesByWard;

/// <summary>
///     GetAddressesByWard Response
/// </summary>
public class GetAddressesByWardResponse : IFeatureResponse
{
    public GetAddressesByWardResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Address> Addresses { get; init; }

        public sealed class Address
        {
            public string Ward { get; init; }
            public string District { get; init; }
            public string Province { get; init; }
        }
    }
}
