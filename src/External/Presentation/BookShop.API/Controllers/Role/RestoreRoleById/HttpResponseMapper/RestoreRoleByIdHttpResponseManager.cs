using System;
using System.Collections.Generic;
using BookShop.Application.Features.Roles.RestoreRoleById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Role.RestoreRoleById.HttpResponseMapper;

/// <summary>
///     Mapper for RestoreRoleById feature
/// </summary>
public class RestoreRoleByIdHttpResponseManager
{
    private readonly Dictionary<
        RestoreRoleByIdResponseStatusCode,
        Func<RestoreRoleByIdRequest, RestoreRoleByIdResponse, RestoreRoleByIdHttpResponse>
    > _dictionary;

    internal RestoreRoleByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreRoleByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreRoleByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreRoleByIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RestoreRoleByIdResponseStatusCode.ROLE_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RestoreRoleByIdRequest,
        RestoreRoleByIdResponse,
        RestoreRoleByIdHttpResponse
    > Resolve(RestoreRoleByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
