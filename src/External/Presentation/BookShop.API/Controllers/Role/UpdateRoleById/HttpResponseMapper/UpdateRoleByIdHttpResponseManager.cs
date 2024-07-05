using System;
using System.Collections.Generic;
using BookShop.Application.Features.Roles.UpdateRoleById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Role.UpdateRoleById.HttpResponseMapper;

/// <summary>
///     Mapper for UpdateRoleById feature
/// </summary>
public class UpdateRoleByIdHttpResponseManager
{
    private readonly Dictionary<
        UpdateRoleByIdResponseStatusCode,
        Func<UpdateRoleByIdRequest, UpdateRoleByIdResponse, UpdateRoleByIdHttpResponse>
    > _dictionary;

    internal UpdateRoleByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateRoleByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateRoleByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateRoleByIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateRoleByIdResponseStatusCode.ROLE_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateRoleByIdResponseStatusCode.ROLE_NAME_IS_ALREADY_EXISTS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateRoleByIdRequest,
        UpdateRoleByIdResponse,
        UpdateRoleByIdHttpResponse
    > Resolve(UpdateRoleByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
