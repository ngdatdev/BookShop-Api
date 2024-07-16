using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.RestoreOrderStatusToConfirm.HttpResponseMapper;

/// <summary>
///     Mapper for RestoreOrderStatusToConfirm feature
/// </summary>
public class RestoreOrderStatusToConfirmHttpResponseManager
{
    private readonly Dictionary<
        RestoreOrderStatusToConfirmResponseStatusCode,
        Func<
            RestoreOrderStatusToConfirmRequest,
            RestoreOrderStatusToConfirmResponse,
            RestoreOrderStatusToConfirmHttpResponse
        >
    > _dictionary;

    internal RestoreOrderStatusToConfirmHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreOrderStatusToConfirmResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderStatusToConfirmResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderStatusToConfirmResponseStatusCode.ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderStatusToConfirmResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreOrderStatusToConfirmResponseStatusCode.ORDER_STATUS_IS_NOT_CANCELED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RestoreOrderStatusToConfirmRequest,
        RestoreOrderStatusToConfirmResponse,
        RestoreOrderStatusToConfirmHttpResponse
    > Resolve(RestoreOrderStatusToConfirmResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
