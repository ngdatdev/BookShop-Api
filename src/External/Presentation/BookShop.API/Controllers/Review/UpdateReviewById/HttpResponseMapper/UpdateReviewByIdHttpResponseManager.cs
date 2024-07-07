using System;
using System.Collections.Generic;
using BookShop.Application.Features.Reviews.UpdateReviewById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Review.UpdateReviewById.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class UpdateReviewByIdHttpResponseManager
{
    private readonly Dictionary<
        UpdateReviewByIdResponseStatusCode,
        Func<UpdateReviewByIdRequest, UpdateReviewByIdResponse, UpdateReviewByIdHttpResponse>
    > _dictionary;

    internal UpdateReviewByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateReviewByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateReviewByIdResponseStatusCode.REVIEW_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateReviewByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateReviewByIdRequest,
        UpdateReviewByIdResponse,
        UpdateReviewByIdHttpResponse
    > Resolve(UpdateReviewByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
