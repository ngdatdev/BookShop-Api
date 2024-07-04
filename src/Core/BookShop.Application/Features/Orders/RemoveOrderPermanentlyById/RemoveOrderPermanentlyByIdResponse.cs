using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Orders.RemoveOrderPermanentlyById;

/// <summary>
///     RemoveOrderPermanentlyById Response
/// </summary>
public class RemoveOrderPermanentlyByIdResponse : IFeatureResponse
{
    public RemoveOrderPermanentlyByIdResponseStatusCode StatusCode { get; init; }
}
