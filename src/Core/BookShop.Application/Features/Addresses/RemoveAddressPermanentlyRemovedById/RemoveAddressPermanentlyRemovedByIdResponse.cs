using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///     RemoveAddressPermanentlyRemovedById Response
/// </summary>
public class RemoveAddressPermanentlyRemovedByIdResponse : IFeatureResponse
{
    public RemoveAddressPermanentlyRemovedByIdResponseStatusCode StatusCode { get; init; }
}
