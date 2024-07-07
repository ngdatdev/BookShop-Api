using System;
using System.Collections;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Reviews.GetReviewsByUserId;

/// <summary>
///     GetReviewsByUserId Response
/// </summary>
public class GetReviewsByUserIdResponse : IFeatureResponse
{
    public GetReviewsByUserIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; set; }

    public sealed class Body
    {
        public IEnumerable<Review> Reviews { get; init; }

        public sealed class Review
        {
            public Guid ReviewId { get; init; }
            public string Comment { get; init; }
        }
    }
}
