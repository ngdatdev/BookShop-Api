using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Orders.GetAllTemporarilyRemovedOrder;

/// <summary>
///     GetAllTemporarilyRemovedOrder Request
/// </summary>
public class GetAllTemporarilyRemovedOrderRequest
    : IFeatureRequest<GetAllTemporarilyRemovedOrderResponse>
{
    [FromQuery]
    public int PageIndex { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}
