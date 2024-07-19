using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Orders.CreateOrder;

/// <summary>
///     CreateOrder Request
/// </summary>
public class CreateOrderRequest : IFeatureRequest<CreateOrderResponse>
{
    public IEnumerable<CartItem> CartItems { get; init; }

    public string ShippingAddress { get; set; }

    public sealed class CartItem
    {
        public Guid ProductId { get; init; }

        public int Quantity { get; init; }
    }
}
