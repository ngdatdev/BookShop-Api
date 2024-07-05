using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.GetAllOrderDetailsByUserId;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.GetAllOrderDetailsByUserId.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllOrderDetailsByUserId feature
/// </summary>
public class GetAllOrderDetailsByUserIdHttpResponseManager
{
    private readonly Dictionary<
        GetAllOrderDetailsByUserIdResponseStatusCode,
        Func<
            GetAllOrderDetailsByUserIdRequest,
            GetAllOrderDetailsByUserIdResponse,
            GetAllOrderDetailsByUserIdHttpResponse
        >
    > _dictionary;

    internal GetAllOrderDetailsByUserIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllOrderDetailsByUserIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody,
                }
        );

        _dictionary.Add(
            key: GetAllOrderDetailsByUserIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllOrderDetailsByUserIdRequest,
        GetAllOrderDetailsByUserIdResponse,
        GetAllOrderDetailsByUserIdHttpResponse
    > Resolve(GetAllOrderDetailsByUserIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
