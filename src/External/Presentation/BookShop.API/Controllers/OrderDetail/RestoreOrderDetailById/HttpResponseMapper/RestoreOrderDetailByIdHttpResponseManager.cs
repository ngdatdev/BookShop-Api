using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.RestoreOrderDetailById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.RestoreOrderDetailById.HttpResponseMapper;

/// <summary>
///     Mapper for RestoreOrderDetailById feature
/// </summary>
public class RestoreOrderDetailByIdHttpResponseManager
{
    private readonly Dictionary<
        RestoreOrderDetailByIdResponseStatusCode,
        Func<
            RestoreOrderDetailByIdRequest,
            RestoreOrderDetailByIdResponse,
            RestoreOrderDetailByIdHttpResponse
        >
    > _dictionary;

    internal RestoreOrderDetailByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreOrderDetailByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderDetailByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderDetailByIdResponseStatusCode.ORDER_DETAIL_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderDetailByIdResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RestoreOrderDetailByIdRequest,
        RestoreOrderDetailByIdResponse,
        RestoreOrderDetailByIdHttpResponse
    > Resolve(RestoreOrderDetailByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
