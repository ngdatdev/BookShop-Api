using System;
using System.Collections.Generic;
using BookShop.Application.Features.Roles.RemoveRoleTemporarilyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Role.RemoveRoleTemporarilyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveRoleTemporarilyById feature
/// </summary>
public class RemoveRoleTemporarilyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveRoleTemporarilyByIdResponseStatusCode,
        Func<
            RemoveRoleTemporarilyByIdRequest,
            RemoveRoleTemporarilyByIdResponse,
            RemoveRoleTemporarilyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveRoleTemporarilyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveRoleTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveRoleTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveRoleTemporarilyByIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveRoleTemporarilyByIdResponseStatusCode.ROLE_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveRoleTemporarilyByIdRequest,
        RemoveRoleTemporarilyByIdResponse,
        RemoveRoleTemporarilyByIdHttpResponse
    > Resolve(RemoveRoleTemporarilyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
