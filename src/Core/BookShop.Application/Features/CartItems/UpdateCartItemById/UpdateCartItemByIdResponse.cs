using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.CartItems.UpdateCartItemById;

/// <summary>
///     UpdateCartItemById Response
/// </summary>
public class UpdateCartItemByIdResponse : IFeatureResponse
{
    public UpdateCartItemByIdResponseStatusCode StatusCode { get; init; }
}
