using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.GetOrderDetailsByOrderStatusId;

/// <summary>
///     GetOrderDetailsByOrderStatusId Response
/// </summary>
public class GetOrderDetailsByOrderStatusIdResponse : IFeatureResponse
{
    public GetOrderDetailsByOrderStatusIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
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
