using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Reviews.GetReviewsByProductId;

/// <summary>
///     GetReviewsByProductId Request
/// </summary>
public class GetReviewsByProductIdRequest : IFeatureRequest<GetReviewsByProductIdResponse>
{
    [FromRoute(Name = "product-id")]
    public Guid ProductId { get; init; }

    public int Pageindex { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}
