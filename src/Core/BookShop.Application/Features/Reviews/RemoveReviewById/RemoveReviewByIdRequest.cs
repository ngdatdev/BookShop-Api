using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Reviews.RemoveReviewById;

/// <summary>
///     RemoveReviewById Request
/// </summary>
public class RemoveReviewByIdRequest : IFeatureRequest<RemoveReviewByIdResponse>
{
    public Guid ReviewId { get; init; }
}
