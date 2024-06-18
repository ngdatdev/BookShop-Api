using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     CreateProduct Response
/// </summary>
public class CreateProductResponse : IFeatureResponse
{
    public CreateProductResponseStatusCode StatusCode { get; init; }
}
