using System;
using System.Collections.Generic;
using BookShop.Application.Features.Roles.CreateRole;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Role.CreateRole.HttpResponseMapper;

/// <summary>
///     Mapper for CreateRole feature
/// </summary>
public class CreateRoleHttpResponseManager
{
    private readonly Dictionary<
        CreateRoleResponseStatusCode,
        Func<CreateRoleRequest, CreateRoleResponse, CreateRoleHttpResponse>
    > _dictionary;

    internal CreateRoleHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.ROLE_ALREADY_EXISTS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status409Conflict,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateRoleResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<CreateRoleRequest, CreateRoleResponse, CreateRoleHttpResponse> Resolve(
        CreateRoleResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
