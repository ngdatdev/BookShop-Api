using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///     RestoreOrderStatusToConfirm Request
/// </summary>
public class RestoreOrderStatusToConfirmRequest
    : IFeatureRequest<RestoreOrderStatusToConfirmResponse>
{
    [FromRoute(Name = "order-detail-id")]
    public Guid OrderDetailId { get; init; }
}
