using System;
using System.Collections.Generic;
using System.Linq;
using BookShop.Application.Features.Payments.GetPaymentsByMethod;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Payments.GetPaymentsByMethod.HttpResponseMapper;

/// <summary>
///     Mapper for GetPaymentsByMethod feature
/// </summary>
public class GetPaymentsByMethodHttpResponseManager
{
    private readonly Dictionary<
        GetPaymentsByMethodResponseStatusCode,
        Func<
            GetPaymentsByMethodRequest,
            GetPaymentsByMethodResponse,
            GetPaymentsByMethodHttpResponse
        >
    > _dictionary;

    internal GetPaymentsByMethodHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetPaymentsByMethodResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<
        GetPaymentsByMethodRequest,
        GetPaymentsByMethodResponse,
        GetPaymentsByMethodHttpResponse
    > Resolve(GetPaymentsByMethodResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
