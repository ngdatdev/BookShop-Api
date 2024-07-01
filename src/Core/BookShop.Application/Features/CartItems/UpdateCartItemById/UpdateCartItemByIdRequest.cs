using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.CartItems.UpdateCartItemById;

/// <summary>
///     UpdateCartItemById Request
/// </summary>
public class UpdateCartItemByIdRequest : IFeatureRequest<UpdateCartItemByIdResponse>
{
    public Guid CartItemId { get; init; }
    public int Quantity { get; init; } = 1;
}
