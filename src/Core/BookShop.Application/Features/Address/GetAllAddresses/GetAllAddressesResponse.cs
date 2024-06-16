using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Address.GetAllAddresses;

/// <summary>
///     GetAllAddresses Response
/// </summary>
public class GetAllAddressesResponse : IFeatureResponse
{
    public GetAllAddressesResponseStatusCode StatusCode { get; init; }

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
