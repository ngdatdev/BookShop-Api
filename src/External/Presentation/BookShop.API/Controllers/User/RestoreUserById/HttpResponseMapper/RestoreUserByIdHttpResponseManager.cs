using System;
using System.Collections.Generic;
using BookShop.Application.Features.Users.RestoreUserById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.User.RestoreUserById.HttpResponseMapper;

/// <summary>
///     Mapper for RestoreUserById feature
/// </summary>
public class RestoreUserByIdHttpResponseManager
{
    private readonly Dictionary<
        RestoreUserByIdResponseStatusCode,
        Func<RestoreUserByIdRequest, RestoreUserByIdResponse, RestoreUserByIdHttpResponse>
    > _dictionary;

    internal RestoreUserByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreUserByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreUserByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreUserByIdResponseStatusCode.USER_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreUserByIdResponseStatusCode.USER_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        RestoreUserByIdRequest,
        RestoreUserByIdResponse,
        RestoreUserByIdHttpResponse
    > Resolve(RestoreUserByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
