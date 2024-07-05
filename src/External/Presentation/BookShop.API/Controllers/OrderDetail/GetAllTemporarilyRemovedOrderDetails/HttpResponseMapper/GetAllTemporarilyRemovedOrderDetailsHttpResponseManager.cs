using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.GetAllTemporarilyRemovedOrderDetails;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.GetAllTemporarilyRemovedOrderDetails.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllTemporarilyRemovedOrderDetails feature
/// </summary>
public class GetAllTemporarilyRemovedOrderDetailsHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedOrderDetailsResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedOrderDetailsRequest,
            GetAllTemporarilyRemovedOrderDetailsResponse,
            GetAllTemporarilyRemovedOrderDetailsHttpResponse
        >
    > _dictionary;

    internal GetAllTemporarilyRemovedOrderDetailsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedOrderDetailsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetAllTemporarilyRemovedOrderDetailsResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllTemporarilyRemovedOrderDetailsRequest,
        GetAllTemporarilyRemovedOrderDetailsResponse,
        GetAllTemporarilyRemovedOrderDetailsHttpResponse
    > Resolve(GetAllTemporarilyRemovedOrderDetailsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
