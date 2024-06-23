using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.Product.UpdateProductById;

/// <summary>
///     UpdateProductById Response
/// </summary>
public class UpdateProductByIdResponse : IFeatureResponse
{
    public UpdateProductByIdResponseStatusCode StatusCode { get; init; }
}
