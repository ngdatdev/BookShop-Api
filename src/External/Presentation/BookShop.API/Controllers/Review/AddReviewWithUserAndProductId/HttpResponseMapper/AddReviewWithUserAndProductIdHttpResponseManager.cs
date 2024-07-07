using System;
using System.Collections.Generic;
using BookShop.Application.Features.Reviews.AddReviewWithUserAndProductId;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Review.AddReviewWithUserAndProductId.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class AddReviewWithUserAndProductIdHttpResponseManager
{
    private readonly Dictionary<
        AddReviewWithUserAndProductIdResponseStatusCode,
        Func<
            AddReviewWithUserAndProductIdRequest,
            AddReviewWithUserAndProductIdResponse,
            AddReviewWithUserAndProductIdHttpResponse
        >
    > _dictionary;

    internal AddReviewWithUserAndProductIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: AddReviewWithUserAndProductIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddReviewWithUserAndProductIdResponseStatusCode.PRODUCT_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddReviewWithUserAndProductIdResponseStatusCode.PRODUCT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddReviewWithUserAndProductIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        AddReviewWithUserAndProductIdRequest,
        AddReviewWithUserAndProductIdResponse,
        AddReviewWithUserAndProductIdHttpResponse
    > Resolve(AddReviewWithUserAndProductIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
