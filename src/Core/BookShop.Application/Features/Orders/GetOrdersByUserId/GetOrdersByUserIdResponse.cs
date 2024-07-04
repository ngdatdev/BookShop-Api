using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Orders.GetOrdersByUserId;

/// <summary>
///     GetOrdersByUserId Response
/// </summary>
public class GetOrdersByUserIdResponse : IFeatureResponse
{
    public GetOrdersByUserIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public IEnumerable<Order> Orders { get; init; }

        public string TotalOrderCost { get; init; }

        public sealed class Order
        {
            public string FullNameUser { get; init; }

            public DateTime OrderDate { get; init; }

            public string TotalCost { get; init; }

            public string Address { get; init; }

            public IEnumerable<OrderDetail> OrderDetails { get; init; }

            public sealed class OrderDetail
            {
                public Guid Id { get; init; }

                public int Quantity { get; init; }

                public string Cost { get; init; }

                public string OrderStatus { get; init; }

                public string NameProduct { get; init; }

                public string ImageUrl { get; init; }

                public string Author { get; init; }

                public string Price { get; init; }
            }
        }
    }
}
