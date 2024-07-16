using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToNext.HttpResponseMapper;

/// <summary>
///     Mapper for SwitchOrderStatusToNext feature
/// </summary>
public class SwitchOrderStatusToNextHttpResponseManager
{
    private readonly Dictionary<
        SwitchOrderStatusToNextResponseStatusCode,
        Func<
            SwitchOrderStatusToNextRequest,
            SwitchOrderStatusToNextResponse,
            SwitchOrderStatusToNextHttpResponse
        >
    > _dictionary;

    internal SwitchOrderStatusToNextHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: SwitchOrderStatusToNextResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchOrderStatusToNextResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchOrderStatusToNextResponseStatusCode.ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchOrderStatusToNextResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchOrderStatusToNextResponseStatusCode.ORDER_STATUS_IS_END,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        SwitchOrderStatusToNextRequest,
        SwitchOrderStatusToNextResponse,
        SwitchOrderStatusToNextHttpResponse
    > Resolve(SwitchOrderStatusToNextResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
