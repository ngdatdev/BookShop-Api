using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.CartItems.RemoveCartItemById;

/// <summary>
///     RemoveCartItemById Response
/// </summary>
public class RemoveCartItemByIdResponse : IFeatureResponse
{
    public RemoveCartItemByIdResponseStatusCode StatusCode { get; init; }
}
