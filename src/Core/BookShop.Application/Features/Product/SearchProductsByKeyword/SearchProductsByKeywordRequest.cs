using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Product.SearchProductsByKeyword;

/// <summary>
///     SearchProductsByKeyword Request
/// </summary>
public class SearchProductsByKeywordRequest : IFeatureRequest<SearchProductsByKeywordResponse>
{
    [FromQuery]
    public string Keyword { get; init; }

    [FromQuery]
    public int PageIndex { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 20;

    [FromQuery]
    public string SortField { get; init; }

    [FromQuery]
    public string Order { get; init; } = "asc";

    [FromQuery]
    public decimal? MinPrice { get; init; }

    [FromQuery]
    public decimal? MaxPrice { get; init; }

    [FromQuery]
    public Dictionary<string, string> Filters { get; set; }
}
