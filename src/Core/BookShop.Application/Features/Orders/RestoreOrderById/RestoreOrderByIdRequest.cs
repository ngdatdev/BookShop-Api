using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Orders.RestoreOrderById;

/// <summary>
///     RestoreOrderById Request
/// </summary>
public class RestoreOrderByIdRequest : IFeatureRequest<RestoreOrderByIdResponse>
{
    [FromRoute(Name = "order-id")]
    public Guid OrderId { get; init; }
}
