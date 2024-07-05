using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.GetOrderDetailById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailById.HttpResponseMapper;

/// <summary>
///     Mapper for GetOrderDetailById feature
/// </summary>
public class GetOrderDetailByIdHttpResponseManager
{
    private readonly Dictionary<
        GetOrderDetailByIdResponseStatusCode,
        Func<GetOrderDetailByIdRequest, GetOrderDetailByIdResponse, GetOrderDetailByIdHttpResponse>
    > _dictionary;

    internal GetOrderDetailByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetOrderDetailByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetOrderDetailByIdResponseStatusCode.ORDER_DETAIL_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetOrderDetailByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetOrderDetailByIdResponseStatusCode.ORDER_DETAIL_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetOrderDetailByIdRequest,
        GetOrderDetailByIdResponse,
        GetOrderDetailByIdHttpResponse
    > Resolve(GetOrderDetailByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
