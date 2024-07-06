using System;
using System.Collections.Generic;
using BookShop.Application.Features.Addresses.GetAddressesByWard;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Address.GetAddressesByWard.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class GetAddressesByWardHttpResponseManager
{
    private readonly Dictionary<
        GetAddressesByWardResponseStatusCode,
        Func<GetAddressesByWardRequest, GetAddressesByWardResponse, GetAddressesByWardHttpResponse>
    > _dictionary;

    internal GetAddressesByWardHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAddressesByWardResponseStatusCode.OPERATION_SUCCESS,
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
        GetAddressesByWardRequest,
        GetAddressesByWardResponse,
        GetAddressesByWardHttpResponse
    > Resolve(GetAddressesByWardResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
