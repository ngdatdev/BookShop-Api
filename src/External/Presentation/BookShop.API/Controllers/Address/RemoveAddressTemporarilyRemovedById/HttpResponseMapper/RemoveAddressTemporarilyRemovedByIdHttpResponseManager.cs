using System;
using System.Collections.Generic;
using BookShop.Application.Features.Addresses.RemoveAddressTemporarilyRemovedById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Address.RemoveAddressTemporarilyRemovedById.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class RemoveAddressTemporarilyRemovedByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveAddressTemporarilyRemovedByIdResponseStatusCode,
        Func<
            RemoveAddressTemporarilyRemovedByIdRequest,
            RemoveAddressTemporarilyRemovedByIdResponse,
            RemoveAddressTemporarilyRemovedByIdHttpResponse
        >
    > _dictionary;

    internal RemoveAddressTemporarilyRemovedByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveAddressTemporarilyRemovedByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveAddressTemporarilyRemovedByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveAddressTemporarilyRemovedByIdResponseStatusCode.ADDRESS_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveAddressTemporarilyRemovedByIdResponseStatusCode.ADDRESS_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveAddressTemporarilyRemovedByIdRequest,
        RemoveAddressTemporarilyRemovedByIdResponse,
        RemoveAddressTemporarilyRemovedByIdHttpResponse
    > Resolve(RemoveAddressTemporarilyRemovedByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
