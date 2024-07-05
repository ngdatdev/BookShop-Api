using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.GetOrderDetailsByOrderStatusId;

/// <summary>
///     GetOrderDetailsByOrderStatusId Request
/// </summary>
public class GetOrderDetailsByOrderStatusIdRequest
    : IFeatureRequest<GetOrderDetailsByOrderStatusIdResponse>
{
    public Guid OrderStatusId { get; set; }
}
