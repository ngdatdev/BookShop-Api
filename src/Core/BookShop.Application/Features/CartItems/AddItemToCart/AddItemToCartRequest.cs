using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.CartItems.AddItemToCart;

/// <summary>
///     AddItemToCart Request
/// </summary>
public class AddItemToCartRequest : IFeatureRequest<AddItemToCartResponse>
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; } = 1;
}
