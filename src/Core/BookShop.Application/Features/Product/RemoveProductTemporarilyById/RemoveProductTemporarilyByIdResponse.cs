using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Product.RemoveProductTemporarilyById;

/// <summary>
///     RemoveProductTemporarilyById Response
/// </summary>
public class RemoveProductTemporarilyByIdResponse : IFeatureResponse
{
    public RemoveProductTemporarilyByIdResponseStatusCode StatusCode { get; init; }
}
