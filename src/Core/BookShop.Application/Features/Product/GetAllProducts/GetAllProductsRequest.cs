using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Product.GetAllProducts;

/// <summary>
///     GetAllProducts Request
/// </summary>
public class GetAllProductsRequest : IFeatureRequest<GetAllProductsResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
