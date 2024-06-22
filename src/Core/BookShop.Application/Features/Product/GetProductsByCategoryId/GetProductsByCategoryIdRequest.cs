using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Product.GetProductsByCategoryId;

/// <summary>
///     GetProductsByCategoryId Request
/// </summary>
public class GetProductsByCategoryIdRequest : IFeatureRequest<GetProductsByCategoryIdResponse>
{
    public Guid CategoryId { get; init; }

    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 20;

    public string SortField { get; init; }

    public string Order { get; init; } = "desc";

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }
}
