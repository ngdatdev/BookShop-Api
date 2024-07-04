using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Orders.RemoveOrderPermanentlyById;

/// <summary>
///     RemoveOrderPermanentlyById Request
/// </summary>
public class RemoveOrderPermanentlyByIdRequest : IFeatureRequest<RemoveOrderPermanentlyByIdResponse>
{
    [FromRoute(Name = "order-id")]
    public Guid OrderId { get; init; }
}
