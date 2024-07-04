using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Orders.GetOrderById;

/// <summary>
///     GetOrderById Request
/// </summary>
public class GetOrderByIdRequest : IFeatureRequest<GetOrderByIdResponse>
{
    public Guid OrderId { get; set; }
}
