using System;
using System.Collections.Generic;
using BookShop.Application.Features.Orders.GetAllTemporarilyRemovedOrder;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Order.GetAllTemporarilyRemovedOrder.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllTemporarilyRemovedOrder feature
/// </summary>
public class GetAllTemporarilyRemovedOrderHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedOrderResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedOrderRequest,
            GetAllTemporarilyRemovedOrderResponse,
            GetAllTemporarilyRemovedOrderHttpResponse
        >
    > _dictionary;

    internal GetAllTemporarilyRemovedOrderHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedOrderResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetAllTemporarilyRemovedOrderResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllTemporarilyRemovedOrderRequest,
        GetAllTemporarilyRemovedOrderResponse,
        GetAllTemporarilyRemovedOrderHttpResponse
    > Resolve(GetAllTemporarilyRemovedOrderResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
