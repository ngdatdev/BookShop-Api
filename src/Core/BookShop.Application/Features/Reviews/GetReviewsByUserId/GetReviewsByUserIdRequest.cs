using System;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Application.Features.Reviews.GetReviewsByUserId;

/// <summary>
///     GetReviewsByUserId Request
/// </summary>
public class GetReviewsByUserIdRequest : IFeatureRequest<GetReviewsByUserIdResponse>
{
    [FromRoute(Name = "user-id")]
    public Guid UserId { get; init; }

    [FromQuery]
    public int Pageindex { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}
