using System;
using System.Collections.Generic;
using BookShop.Application.Features.Payments.UpdatePaymentByWebHook;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Payments.UpdatePaymentByWebHook.HttpResponseMapper;

/// <summary>
///     Mapper for UpdatePaymentByWebHook feature
/// </summary>
public class UpdatePaymentByWebHookHttpResponseManager
{
    private readonly Dictionary<
        UpdatePaymentByWebHookResponseStatusCode,
        Func<
            UpdatePaymentByWebHookRequest,
            UpdatePaymentByWebHookResponse,
            UpdatePaymentByWebHookHttpResponse
        >
    > _dictionary;

    internal UpdatePaymentByWebHookHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdatePaymentByWebHookResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePaymentByWebHookResponseStatusCode.WEBHOOK_RETURN_ERROR,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePaymentByWebHookResponseStatusCode.ORDER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePaymentByWebHookResponseStatusCode.SIGNATURE_IS_NOT_VALID,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdatePaymentByWebHookRequest,
        UpdatePaymentByWebHookResponse,
        UpdatePaymentByWebHookHttpResponse
    > Resolve(UpdatePaymentByWebHookResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
