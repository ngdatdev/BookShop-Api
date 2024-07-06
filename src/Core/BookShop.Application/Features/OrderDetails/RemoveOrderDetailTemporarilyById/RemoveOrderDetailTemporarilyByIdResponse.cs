using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///     RemoveOrderDetailTemporarilyById Response
/// </summary>
public class RemoveOrderDetailTemporarilyByIdResponse : IFeatureResponse
{
    public RemoveOrderDetailTemporarilyByIdResponseStatusCode StatusCode { get; init; }
}
