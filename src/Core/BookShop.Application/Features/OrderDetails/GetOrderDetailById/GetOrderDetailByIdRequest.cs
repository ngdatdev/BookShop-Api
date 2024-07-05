using System;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.GetOrderDetailById;

/// <summary>
///     GetOrderDetailById Request
/// </summary>
public class GetOrderDetailByIdRequest : IFeatureRequest<GetOrderDetailByIdResponse>
{
    public Guid OrderDetailId { get; set; }
}
