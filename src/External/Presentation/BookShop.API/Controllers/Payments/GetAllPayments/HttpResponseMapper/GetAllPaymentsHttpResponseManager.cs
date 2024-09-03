using System;
using System.Collections.Generic;
using System.Linq;
using BookShop.Application.Features.Payments.GetAllPayments;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Payments.GetAllPayments.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllPayments feature
/// </summary>
public class GetAllPaymentsHttpResponseManager
{
    private readonly Dictionary<
        GetAllPaymentsResponseStatusCode,
        Func<GetAllPaymentsRequest, GetAllPaymentsResponse, GetAllPaymentsHttpResponse>
    > _dictionary;

    internal GetAllPaymentsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllPaymentsResponseStatusCode.OPERATION_SUCCESS,
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
        GetAllPaymentsRequest,
        GetAllPaymentsResponse,
        GetAllPaymentsHttpResponse
    > Resolve(GetAllPaymentsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
