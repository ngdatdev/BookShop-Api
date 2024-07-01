using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Carts.GetCartByUserId;

/// <summary>
///     GetCartByUserId Response
/// </summary>
public class GetCartByUserIdResponse : IFeatureResponse
{
    public GetCartByUserIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public Guid CartId { get; init; }

        public int NumberOfItems { get; init; }

        public string TotalPrice { get; init; }

        public string FinalPrice { get; init; }

        public IEnumerable<CartItem> CartItems { get; init; }

        public sealed class CartItem
        {
            public string FullName { get; init; }

            public string ImageUrl { get; init; }

            public string Size { get; init; }

            public string RootPrice { get; init; }

            public string DiscountPrice { get; init; }

            public string Author { get; init; }

            public int Quantity { get; init; }
        }
    }
}
