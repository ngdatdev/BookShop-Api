using System;
using System.Collections.Generic;
using BookShop.Application.Features.Users.RemoveUserTemporarilyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.User.RemoveUserTemporarilyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveUserTemporarilyById feature
/// </summary>
public class RemoveUserTemporarilyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveUserTemporarilyByIdResponseStatusCode,
        Func<
            RemoveUserTemporarilyByIdRequest,
            RemoveUserTemporarilyByIdResponse,
            RemoveUserTemporarilyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveUserTemporarilyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveUserTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveUserTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveUserTemporarilyByIdResponseStatusCode.USER_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveUserTemporarilyByIdResponseStatusCode.USER_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        RemoveUserTemporarilyByIdRequest,
        RemoveUserTemporarilyByIdResponse,
        RemoveUserTemporarilyByIdHttpResponse
    > Resolve(RemoveUserTemporarilyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
