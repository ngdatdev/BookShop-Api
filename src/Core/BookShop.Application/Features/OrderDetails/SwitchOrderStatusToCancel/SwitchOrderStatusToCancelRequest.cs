using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///     SwitchOrderStatusToCancel Request
/// </summary>
public class SwitchOrderStatusToCancelRequest : IFeatureRequest<SwitchOrderStatusToCancelResponse>
{
    [FromRoute(Name = "order-detail-id")]
    public Guid OrderDetailId { get; init; }
}
