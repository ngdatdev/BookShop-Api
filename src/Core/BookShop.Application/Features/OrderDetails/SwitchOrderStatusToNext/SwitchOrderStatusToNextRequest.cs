using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///     SwitchOrderStatusToNext Request
/// </summary>
public class SwitchOrderStatusToNextRequest
    : IFeatureRequest<SwitchOrderStatusToNextResponse>
{
    [FromRoute(Name = "order-detail-id")]
    public Guid OrderDetailId { get; init; }
}
