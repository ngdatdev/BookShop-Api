using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///     RemoveAddressTemporarilyRemovedById Response
/// </summary>
public class RemoveAddressTemporarilyRemovedByIdResponse : IFeatureResponse
{
    public RemoveAddressTemporarilyRemovedByIdResponseStatusCode StatusCode { get; init; }
}
