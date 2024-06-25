using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Product.RestoreProductById;

/// <summary>
///     RestoreProductById Response
/// </summary>
public class RestoreProductByIdResponse : IFeatureResponse
{
    public RestoreProductByIdResponseStatusCode StatusCode { get; init; }
}
