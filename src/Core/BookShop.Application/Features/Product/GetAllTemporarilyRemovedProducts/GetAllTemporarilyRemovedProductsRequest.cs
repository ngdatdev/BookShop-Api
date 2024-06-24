using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Product.GetAllTemporarilyRemovedProducts;

/// <summary>
///     GetAllTemporarilyRemovedProducts Request
/// </summary>
public class GetAllTemporarilyRemovedProductsRequest
    : IFeatureRequest<GetAllTemporarilyRemovedProductsResponse>
{
    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
