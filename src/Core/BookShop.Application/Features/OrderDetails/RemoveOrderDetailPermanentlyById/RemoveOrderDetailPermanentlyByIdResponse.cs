using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///     RemoveOrderDetailPermanentlyById Response
/// </summary>
public class RemoveOrderDetailPermanentlyByIdResponse : IFeatureResponse
{
    public RemoveOrderDetailPermanentlyByIdResponseStatusCode StatusCode { get; init; }
}
