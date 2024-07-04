using System;
using System.Collections.Generic;
using BookShop.Application.Features.Orders.GetOrderById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Order.GetOrderById.HttpResponseMapper;

/// <summary>
///     Mapper for GetOrderById feature
/// </summary>
public class GetOrderByIdHttpResponseManager
{
    private readonly Dictionary<
        GetOrderByIdResponseStatusCode,
        Func<GetOrderByIdRequest, GetOrderByIdResponse, GetOrderByIdHttpResponse>
    > _dictionary;

    internal GetOrderByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetOrderByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetOrderByIdResponseStatusCode.ORDER_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetOrderByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<GetOrderByIdRequest, GetOrderByIdResponse, GetOrderByIdHttpResponse> Resolve(
        GetOrderByIdResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
