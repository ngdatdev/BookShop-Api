using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Addresses.RestoreAddressById;

/// <summary>
///     RestoreAddressById Request
/// </summary>
public class RestoreAddressByIdRequest : IFeatureRequest<RestoreAddressByIdResponse>
{
    [FromRoute(Name = "address-id")]
    public Guid AddressId { get; init; }
}
