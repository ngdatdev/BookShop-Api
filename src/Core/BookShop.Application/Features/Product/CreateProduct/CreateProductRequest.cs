using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Product.CreateProduct;

/// <summary>
///     CreateProduct Request
/// </summary>
public class CreateProductRequest : IFeatureRequest<CreateProductResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
