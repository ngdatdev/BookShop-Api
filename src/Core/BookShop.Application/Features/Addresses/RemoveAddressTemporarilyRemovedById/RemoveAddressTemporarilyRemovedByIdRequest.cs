using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///     RemoveAddressTemporarilyRemovedById Request
/// </summary>
public class RemoveAddressTemporarilyRemovedByIdRequest
    : IFeatureRequest<RemoveAddressTemporarilyRemovedByIdResponse>
{
    [FromRoute(Name = "address-id")]
    public Guid AddressId { get; init; }
}
