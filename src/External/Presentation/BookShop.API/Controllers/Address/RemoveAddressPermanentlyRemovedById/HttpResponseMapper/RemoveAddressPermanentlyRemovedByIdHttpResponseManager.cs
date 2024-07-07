using System;
using System.Collections.Generic;
using BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Address.RemoveAddressPermanentlyRemovedById.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class RemoveAddressPermanentlyRemovedByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveAddressPermanentlyRemovedByIdResponseStatusCode,
        Func<
            RemoveAddressPermanentlyRemovedByIdRequest,
            RemoveAddressPermanentlyRemovedByIdResponse,
            RemoveAddressPermanentlyRemovedByIdHttpResponse
        >
    > _dictionary;

    internal RemoveAddressPermanentlyRemovedByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveAddressPermanentlyRemovedByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveAddressPermanentlyRemovedByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveAddressPermanentlyRemovedByIdResponseStatusCode.ADDRESS_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveAddressPermanentlyRemovedByIdResponseStatusCode.ADDRESS_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveAddressPermanentlyRemovedByIdRequest,
        RemoveAddressPermanentlyRemovedByIdResponse,
        RemoveAddressPermanentlyRemovedByIdHttpResponse
    > Resolve(RemoveAddressPermanentlyRemovedByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
