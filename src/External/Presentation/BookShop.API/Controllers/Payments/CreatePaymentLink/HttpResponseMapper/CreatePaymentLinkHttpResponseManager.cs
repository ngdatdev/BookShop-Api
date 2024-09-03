using System;
using System.Collections.Generic;
using System.Linq;
using BookShop.Application.Features.Payments.CreatePaymentLink;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Payments.CreatePaymentLink.HttpResponseMapper;

/// <summary>
///     Mapper for CreatePaymentLink feature
/// </summary>
public class CreatePaymentLinkHttpResponseManager
{
    private readonly Dictionary<
        CreatePaymentLinkResponseStatusCode,
        Func<CreatePaymentLinkRequest, CreatePaymentLinkResponse, CreatePaymentLinkHttpResponse>
    > _dictionary;

    internal CreatePaymentLinkHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreatePaymentLinkResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreatePaymentLinkResponseStatusCode.CREATE_URL_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        CreatePaymentLinkRequest,
        CreatePaymentLinkResponse,
        CreatePaymentLinkHttpResponse
    > Resolve(CreatePaymentLinkResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
