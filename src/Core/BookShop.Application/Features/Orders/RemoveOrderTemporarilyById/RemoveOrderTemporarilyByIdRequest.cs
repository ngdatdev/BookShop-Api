using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Orders.RemoveOrderTemporarilyById;

/// <summary>
///     RemoveOrderTemporarilyById Request
/// </summary>
public class RemoveOrderTemporarilyByIdRequest : IFeatureRequest<RemoveOrderTemporarilyByIdResponse>
{
    [FromRoute(Name = "order-id")]
    public Guid OrderId { get; init; }
}
