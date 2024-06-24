using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Product.GetProductsByCategoryId;

/// <summary>
///     GetProductsByCategoryId Request
/// </summary>
public class GetProductsByCategoryIdRequest : IFeatureRequest<GetProductsByCategoryIdResponse>
{
    [FromRoute(Name = "category-id")]
    public Guid CategoryId { get; init; }

    [FromQuery]
    public int PageIndex { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 20;

    [FromQuery]
    public string SortField { get; init; }

    [FromQuery]
    public string Order { get; init; } = "desc";

    [FromQuery]
    public decimal? MinPrice { get; init; }

    [FromQuery]
    public decimal? MaxPrice { get; init; }
}
