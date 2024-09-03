using System;
using System.Collections.Generic;
using BookShop.Application.Features.Payments.UpdatePaymentCOD;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Payments.UpdatePaymentCOD.HttpResponseMapper;

/// <summary>
///     Mapper for UpdatePaymentCOD feature
/// </summary>
public class UpdatePaymentCODHttpResponseManager
{
    private readonly Dictionary<
        UpdatePaymentCODResponseStatusCode,
        Func<UpdatePaymentCODRequest, UpdatePaymentCODResponse, UpdatePaymentCODHttpResponse>
    > _dictionary;

    internal UpdatePaymentCODHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdatePaymentCODResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePaymentCODResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePaymentCODResponseStatusCode.PAYMENT_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdatePaymentCODResponseStatusCode.PAYMENT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdatePaymentCODRequest,
        UpdatePaymentCODResponse,
        UpdatePaymentCODHttpResponse
    > Resolve(UpdatePaymentCODResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
