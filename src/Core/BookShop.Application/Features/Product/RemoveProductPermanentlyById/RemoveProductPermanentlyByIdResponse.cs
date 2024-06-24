using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Product.RemoveProductPermanentlyById;

/// <summary>
///     RemoveProductPermanentlyById Response
/// </summary>
public class RemoveProductPermanentlyByIdResponse : IFeatureResponse
{
    public RemoveProductPermanentlyByIdResponseStatusCode StatusCode { get; init; }
}
