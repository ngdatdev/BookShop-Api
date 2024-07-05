using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.OrderDetails.GetAllTemporarilyRemovedOrderDetails;

/// <summary>
///     GetAllTemporarilyRemovedOrderDetails Request
/// </summary>
public class GetAllTemporarilyRemovedOrderDetailsRequest
    : IFeatureRequest<GetAllTemporarilyRemovedOrderDetailsResponse>
{
    [FromQuery]
    public int PageIndex { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}
