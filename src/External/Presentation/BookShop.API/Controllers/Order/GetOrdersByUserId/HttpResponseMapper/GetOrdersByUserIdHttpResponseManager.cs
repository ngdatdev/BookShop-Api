using System;
using System.Collections.Generic;
using BookShop.Application.Features.Orders.GetOrdersByUserId;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Order.GetOrdersByUserId.HttpResponseMapper;

/// <summary>
///     Mapper for GetOrdersByUserId feature
/// </summary>
public class GetOrdersByUserIdHttpResponseManager
{
    private readonly Dictionary<
        GetOrdersByUserIdResponseStatusCode,
        Func<GetOrdersByUserIdRequest, GetOrdersByUserIdResponse, GetOrdersByUserIdHttpResponse>
    > _dictionary;

    internal GetOrdersByUserIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetOrdersByUserIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetOrdersByUserIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetOrdersByUserIdRequest,
        GetOrdersByUserIdResponse,
        GetOrdersByUserIdHttpResponse
    > Resolve(GetOrdersByUserIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
