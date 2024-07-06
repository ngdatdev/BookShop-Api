using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///     RemoveOrderDetailPermanentlyById Request
/// </summary>
public class RemoveOrderDetailPermanentlyByIdRequest
    : IFeatureRequest<RemoveOrderDetailPermanentlyByIdResponse>
{
    [FromRoute(Name = "order-detail-id")]
    public Guid OrderDetailId { get; init; }
}
