using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.OrderDetails.GetAllTemporarilyRemovedOrderDetails;

/// <summary>
///     GetAllTemporarilyRemovedOrderDetails Response
/// </summary>
public class GetAllTemporarilyRemovedOrderDetailsResponse : IFeatureResponse
{
    public GetAllTemporarilyRemovedOrderDetailsResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<OrderDetail> OrderDetails { get; init; }

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
