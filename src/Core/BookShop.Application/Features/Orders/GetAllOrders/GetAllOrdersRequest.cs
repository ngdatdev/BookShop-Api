using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Orders.GetAllOrders;

/// <summary>
///     GetAllOrders Request
/// </summary>
public class GetAllOrdersRequest : IFeatureRequest<GetAllOrdersResponse>
{
    [FromQuery]
    public int PageIndex { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}
