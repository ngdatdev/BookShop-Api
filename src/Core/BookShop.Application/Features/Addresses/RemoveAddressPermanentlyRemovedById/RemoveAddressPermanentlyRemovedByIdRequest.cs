using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///     RemoveAddressPermanentlyRemovedById Request
/// </summary>
public class RemoveAddressPermanentlyRemovedByIdRequest
    : IFeatureRequest<RemoveAddressPermanentlyRemovedByIdResponse>
{
    [FromRoute(Name = "address-id")]
    public Guid AddressId { get; init; }
}
