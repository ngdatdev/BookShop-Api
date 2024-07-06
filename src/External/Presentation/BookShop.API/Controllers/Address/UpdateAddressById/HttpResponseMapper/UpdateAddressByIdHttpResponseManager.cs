using System;
using System.Collections.Generic;
using BookShop.Application.Features.Addresses.UpdateAddressById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Address.UpdateAddressById.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class UpdateAddressByIdHttpResponseManager
{
    private readonly Dictionary<
        UpdateAddressByIdResponseStatusCode,
        Func<UpdateAddressByIdRequest, UpdateAddressByIdResponse, UpdateAddressByIdHttpResponse>
    > _dictionary;

    internal UpdateAddressByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateAddressByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAddressByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAddressByIdResponseStatusCode.ADDRESS_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateAddressByIdResponseStatusCode.ADDRESS_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateAddressByIdRequest,
        UpdateAddressByIdResponse,
        UpdateAddressByIdHttpResponse
    > Resolve(UpdateAddressByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
