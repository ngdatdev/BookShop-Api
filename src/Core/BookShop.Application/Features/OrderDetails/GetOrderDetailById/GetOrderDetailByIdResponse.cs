using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.GetOrderDetailById;

/// <summary>
///     GetOrderDetailById Response
/// </summary>
public class GetOrderDetailByIdResponse : IFeatureResponse
{
    public GetOrderDetailByIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public DateTime OrderDate { get; init; }

        public string TotalCost { get; init; }

        public string Address { get; init; }

        public string FullNameUser { get; init; }

        public OrderDetail OrderDetailInformation { get; init; }

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
