using System;
using System.Collections;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Reviews.GetReviewsByProductId;

/// <summary>
///     GetReviewsByProductId Response
/// </summary>
public class GetReviewsByProductIdResponse : IFeatureResponse
{
    public GetReviewsByProductIdResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; set; }

    public sealed class Body
    {
        public IEnumerable<Review> Reviews { get; init; }

        public sealed class Review
        {
            public Guid ReviewId { get; init; }
            public string Comment { get; init; }
            public Guid UserId { get; init; }
            public string Fullname { get; init; }
        }
    }
}
