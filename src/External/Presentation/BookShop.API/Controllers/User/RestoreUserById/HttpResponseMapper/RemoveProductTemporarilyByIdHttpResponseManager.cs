using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.RestoreProductById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.RestoreProductById.HttpResponseMapper;

/// <summary>
///     Mapper for RestoreProductById feature
/// </summary>
public class RestoreProductByIdHttpResponseManager
{
    private readonly Dictionary<
        RestoreProductByIdResponseStatusCode,
        Func<RestoreProductByIdRequest, RestoreProductByIdResponse, RestoreProductByIdHttpResponse>
    > _dictionary;

    internal RestoreProductByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreProductByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreProductByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreProductByIdResponseStatusCode.PRODUCT_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreProductByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        RestoreProductByIdRequest,
        RestoreProductByIdResponse,
        RestoreProductByIdHttpResponse
    > Resolve(RestoreProductByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
