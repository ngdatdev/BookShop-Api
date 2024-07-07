using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.GetAllTemporarilyAddresses;

/// <summary>
///     GetAllTemporarilyAddresses Response
/// </summary>
public class GetAllTemporarilyAddressesResponse : IFeatureResponse
{
    public GetAllTemporarilyAddressesResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Address> Addresses { get; init; }

        public sealed class Address
        {
            public Guid AddressId { get; set; }
            public string Ward { get; init; }
            public string District { get; init; }
            public string Province { get; init; }
        }
    }
}
