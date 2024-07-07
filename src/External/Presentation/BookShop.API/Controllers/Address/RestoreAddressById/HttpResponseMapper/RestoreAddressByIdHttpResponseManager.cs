using System;
using System.Collections.Generic;
using BookShop.Application.Features.Addresses.RestoreAddressById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Address.RestoreAddressById.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class RestoreAddressByIdHttpResponseManager
{
    private readonly Dictionary<
        RestoreAddressByIdResponseStatusCode,
        Func<RestoreAddressByIdRequest, RestoreAddressByIdResponse, RestoreAddressByIdHttpResponse>
    > _dictionary;

    internal RestoreAddressByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreAddressByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreAddressByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreAddressByIdResponseStatusCode.ADDRESS_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreAddressByIdResponseStatusCode.ADDRESS_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RestoreAddressByIdRequest,
        RestoreAddressByIdResponse,
        RestoreAddressByIdHttpResponse
    > Resolve(RestoreAddressByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
