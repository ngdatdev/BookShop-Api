using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;

/// <summary>
///     AddReviewWithUserAndProductId Request
/// </summary>
public class AddReviewWithUserAndProductIdRequest
    : IFeatureRequest<AddReviewWithUserAndProductIdResponse>
{
    public Guid ProductId { get; set; }
    public string Comment { get; init; }
}
