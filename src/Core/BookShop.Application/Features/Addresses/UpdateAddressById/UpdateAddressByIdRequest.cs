using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.UpdateAddressById;

/// <summary>
///     UpdateAddressById Request
/// </summary>
public class UpdateAddressByIdRequest : IFeatureRequest<UpdateAddressByIdResponse>
{
    public Guid AddressId { get; init; }

    public string Ward { get; init; }

    public string District { get; init; }

    public string Province { get; init; }
}
