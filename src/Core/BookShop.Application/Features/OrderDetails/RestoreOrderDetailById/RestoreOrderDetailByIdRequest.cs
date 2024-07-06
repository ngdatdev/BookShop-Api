using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.OrderDetails.RestoreOrderDetailById;

/// <summary>
///     RestoreOrderDetailById Request
/// </summary>
public class RestoreOrderDetailByIdRequest : IFeatureRequest<RestoreOrderDetailByIdResponse>
{
    [FromRoute(Name = "order-detail-id")]
    public Guid OrderDetailId { get; init; }
}
