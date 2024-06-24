using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Product.GetProductsByAuthorName;

/// <summary>
///     GetProductsByAuthorName Request
/// </summary>
public class GetProductsByAuthorNameRequest : IFeatureRequest<GetProductsByAuthorNameResponse>
{
    [FromRoute(Name = "author-name")]
    public string AuthorName { get; init; }

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
