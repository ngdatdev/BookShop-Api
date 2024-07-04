using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Orders.RemoveOrderTemporarilyById;

/// <summary>
///     RemoveOrderTemporarilyById Response
/// </summary>
public class RemoveOrderTemporarilyByIdResponse : IFeatureResponse
{
    public RemoveOrderTemporarilyByIdResponseStatusCode StatusCode { get; init; }
}
