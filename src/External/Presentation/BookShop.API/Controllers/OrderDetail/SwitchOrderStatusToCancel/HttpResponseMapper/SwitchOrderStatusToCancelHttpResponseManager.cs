using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.SwitchOrderStatusToCancel.HttpResponseMapper;

/// <summary>
///     Mapper for SwitchOrderStatusToCancel feature
/// </summary>
public class SwitchOrderStatusToCancelHttpResponseManager
{
    private readonly Dictionary<
        SwitchOrderStatusToCancelResponseStatusCode,
        Func<
            SwitchOrderStatusToCancelRequest,
            SwitchOrderStatusToCancelResponse,
            SwitchOrderStatusToCancelHttpResponse
        >
    > _dictionary;

    internal SwitchOrderStatusToCancelHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: SwitchOrderStatusToCancelResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchOrderStatusToCancelResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchOrderStatusToCancelResponseStatusCode.ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchOrderStatusToCancelResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: SwitchOrderStatusToCancelResponseStatusCode.ORDER_STATUS_IS_CAN_NOT_CANCEL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        SwitchOrderStatusToCancelRequest,
        SwitchOrderStatusToCancelResponse,
        SwitchOrderStatusToCancelHttpResponse
    > Resolve(SwitchOrderStatusToCancelResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
