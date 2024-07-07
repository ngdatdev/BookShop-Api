using System;
using System.Collections.Generic;
using BookShop.Application.Features.Reviews.GetReviewsByUserId;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Review.GetReviewsByUserId.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class GetReviewsByUserIdHttpResponseManager
{
    private readonly Dictionary<
        GetReviewsByUserIdResponseStatusCode,
        Func<GetReviewsByUserIdRequest, GetReviewsByUserIdResponse, GetReviewsByUserIdHttpResponse>
    > _dictionary;

    internal GetReviewsByUserIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetReviewsByUserIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetReviewsByUserIdRequest,
        GetReviewsByUserIdResponse,
        GetReviewsByUserIdHttpResponse
    > Resolve(GetReviewsByUserIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
