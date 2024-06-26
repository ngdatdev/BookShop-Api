using System;
using System.Collections.Generic;
using BookShop.Application.Features.Address.GetAllAddresses;
using BookShop.Application.Features.Users.RemoveUserPermanentlyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.User.RemoveUserPermanentlyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveUserPermanentlyById feature
/// </summary>
public class RemoveUserPermanentlyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveUserPermanentlyByIdResponseStatusCode,
        Func<
            RemoveUserPermanentlyByIdRequest,
            RemoveUserPermanentlyByIdResponse,
            RemoveUserPermanentlyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveUserPermanentlyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveUserPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveUserPermanentlyByIdResponseStatusCode.USER_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveUserPermanentlyByIdResponseStatusCode.USER_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveUserPermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveUserPermanentlyByIdRequest,
        RemoveUserPermanentlyByIdResponse,
        RemoveUserPermanentlyByIdHttpResponse
    > Resolve(RemoveUserPermanentlyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
