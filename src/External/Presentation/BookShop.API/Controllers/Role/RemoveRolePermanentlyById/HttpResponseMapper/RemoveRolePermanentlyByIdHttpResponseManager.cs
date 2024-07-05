using System;
using System.Collections.Generic;
using BookShop.Application.Features.Roles.RemoveRolePermanentlyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Role.RemoveRolePermanentlyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveRolePermanentlyById feature
/// </summary>
public class RemoveRolePermanentlyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveRolePermanentlyByIdResponseStatusCode,
        Func<
            RemoveRolePermanentlyByIdRequest,
            RemoveRolePermanentlyByIdResponse,
            RemoveRolePermanentlyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveRolePermanentlyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveRolePermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveRolePermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveRolePermanentlyByIdResponseStatusCode.ROLE_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveRolePermanentlyByIdResponseStatusCode.ROLE_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveRolePermanentlyByIdRequest,
        RemoveRolePermanentlyByIdResponse,
        RemoveRolePermanentlyByIdHttpResponse
    > Resolve(RemoveRolePermanentlyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
