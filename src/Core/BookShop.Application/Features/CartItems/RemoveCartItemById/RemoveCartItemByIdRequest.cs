using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.CartItems.RemoveCartItemById;

/// <summary>
///     RemoveCartItemById Request
/// </summary>
public class RemoveCartItemByIdRequest : IFeatureRequest<RemoveCartItemByIdResponse>
{
    public Guid CartItemId { get; set; }
}
