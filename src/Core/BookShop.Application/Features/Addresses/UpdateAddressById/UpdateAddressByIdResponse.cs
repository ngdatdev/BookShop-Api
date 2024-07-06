using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.UpdateAddressById;

/// <summary>
///     UpdateAddressById Response
/// </summary>
public class UpdateAddressByIdResponse : IFeatureResponse
{
    public UpdateAddressByIdResponseStatusCode StatusCode { get; init; }
}
