using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///     RemoveOrderDetailTemporarilyById Request
/// </summary>
public class RemoveOrderDetailTemporarilyByIdRequest
    : IFeatureRequest<RemoveOrderDetailTemporarilyByIdResponse>
{
    [FromRoute(Name = "order-detail-id")]
    public Guid OrderDetailId { get; init; }
}
