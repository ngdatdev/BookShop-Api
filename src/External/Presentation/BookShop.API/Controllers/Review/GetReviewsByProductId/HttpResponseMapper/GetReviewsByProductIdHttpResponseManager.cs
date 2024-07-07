using System;
using System.Collections.Generic;
using BookShop.Application.Features.Reviews.GetReviewsByProductId;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Review.GetReviewsByProductId.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class GetReviewsByProductIdHttpResponseManager
{
    private readonly Dictionary<
        GetReviewsByProductIdResponseStatusCode,
        Func<
            GetReviewsByProductIdRequest,
            GetReviewsByProductIdResponse,
            GetReviewsByProductIdHttpResponse
        >
    > _dictionary;

    internal GetReviewsByProductIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetReviewsByProductIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetReviewsByProductIdRequest,
        GetReviewsByProductIdResponse,
        GetReviewsByProductIdHttpResponse
    > Resolve(GetReviewsByProductIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
