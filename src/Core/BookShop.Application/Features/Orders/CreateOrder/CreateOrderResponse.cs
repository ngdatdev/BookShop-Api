using BookShop.Application.Shared.Features;
using System;
using System.Collections.Generic;

namespace BookShop.Application.Features.Orders.CreateOrder;

/// <summary>
///     CreateOrder Response
/// </summary>
public class CreateOrderResponse : IFeatureResponse
{
    public CreateOrderResponseStatusCode StatusCode { get; init; }

    public List<Guid> NotFoundProductIds { get; init; }
}
