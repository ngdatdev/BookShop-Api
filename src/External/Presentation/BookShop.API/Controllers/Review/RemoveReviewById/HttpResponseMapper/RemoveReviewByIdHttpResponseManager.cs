using System;
using System.Collections.Generic;
using BookShop.Application.Features.Reviews.RemoveReviewById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Review.RemoveReviewById.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class RemoveReviewByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveReviewByIdResponseStatusCode,
        Func<RemoveReviewByIdRequest, RemoveReviewByIdResponse, RemoveReviewByIdHttpResponse>
    > _dictionary;

    internal RemoveReviewByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveReviewByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveReviewByIdResponseStatusCode.REVIEW_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveReviewByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveReviewByIdRequest,
        RemoveReviewByIdResponse,
        RemoveReviewByIdHttpResponse
    > Resolve(RemoveReviewByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
