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

    public Body ResponseBody { get; set; }

    public sealed class Body
    {
        public Guid CartId { get; set; }

        public int NumberOfItems { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal FinalPrice { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }

        public sealed class CartItem
        {
            public string FullName { get; set; }

            public string ImageUrl { get; set; }

            public string Size { get; set; }

            public string RootPrice { get; set; }

            public string DiscountPrice { get; set; }

            public string Author { get; set; }
        }
    }
}
