using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.GetOrderDetailsByOrderStatusId;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.GetOrderDetailsByOrderStatusId.HttpResponseMapper;

/// <summary>
///     Mapper for GetOrderDetailsByOrderStatusId feature
/// </summary>
public class GetOrderDetailsByOrderStatusIdHttpResponseManager
{
    private readonly Dictionary<
        GetOrderDetailsByOrderStatusIdResponseStatusCode,
        Func<
            GetOrderDetailsByOrderStatusIdRequest,
            GetOrderDetailsByOrderStatusIdResponse,
            GetOrderDetailsByOrderStatusIdHttpResponse
        >
    > _dictionary;

    internal GetOrderDetailsByOrderStatusIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetOrderDetailsByOrderStatusIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetOrderDetailsByOrderStatusIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: GetOrderDetailsByOrderStatusIdResponseStatusCode.ORDER_STATUS_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetOrderDetailsByOrderStatusIdRequest,
        GetOrderDetailsByOrderStatusIdResponse,
        GetOrderDetailsByOrderStatusIdHttpResponse
    > Resolve(GetOrderDetailsByOrderStatusIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
