using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.RestoreAddressById;

/// <summary>
///     RestoreAddressById Response
/// </summary>
public class RestoreAddressByIdResponse : IFeatureResponse
{
    public RestoreAddressByIdResponseStatusCode StatusCode { get; init; }
}
