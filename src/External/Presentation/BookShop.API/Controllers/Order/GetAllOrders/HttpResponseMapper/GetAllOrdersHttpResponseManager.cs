using System;
using System.Collections.Generic;
using BookShop.Application.Features.Orders.GetAllOrders;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Order.GetAllOrders.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllOrders feature
/// </summary>
public class GetAllOrdersHttpResponseManager
{
    private readonly Dictionary<
        GetAllOrdersResponseStatusCode,
        Func<GetAllOrdersRequest, GetAllOrdersResponse, GetAllOrdersHttpResponse>
    > _dictionary;

    internal GetAllOrdersHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllOrdersResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetAllOrdersResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<GetAllOrdersRequest, GetAllOrdersResponse, GetAllOrdersHttpResponse> Resolve(
        GetAllOrdersResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
