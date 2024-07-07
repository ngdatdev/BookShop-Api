using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Reviews.UpdateReviewById;

/// <summary>
///     UpdateReviewById Request
/// </summary>
public class UpdateReviewByIdRequest : IFeatureRequest<UpdateReviewByIdResponse>
{
    public Guid ReviewId { get; init; }
    public string Comment { get; init; }
}
