using System;
using System.Collections.Generic;
using BookShop.Application.Features.Orders.RestoreOrderById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Order.RestoreOrderById.HttpResponseMapper;

/// <summary>
///     Mapper for RestoreOrderById feature
/// </summary>
public class RestoreOrderByIdHttpResponseManager
{
    private readonly Dictionary<
        RestoreOrderByIdResponseStatusCode,
        Func<RestoreOrderByIdRequest, RestoreOrderByIdResponse, RestoreOrderByIdHttpResponse>
    > _dictionary;

    internal RestoreOrderByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreOrderByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderByIdResponseStatusCode.ORDER_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderByIdResponseStatusCode.ORDER_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RestoreOrderByIdRequest,
        RestoreOrderByIdResponse,
        RestoreOrderByIdHttpResponse
    > Resolve(RestoreOrderByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
