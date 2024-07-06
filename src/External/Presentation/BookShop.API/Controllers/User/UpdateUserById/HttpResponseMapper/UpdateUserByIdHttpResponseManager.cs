using System;
using System.Collections.Generic;
using BookShop.Application.Features.Users.UpdateUserById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.User.UpdateUserById.HttpResponseMapper;

/// <summary>
///     Mapper for UpdateUserById feature
/// </summary>
public class UpdateUserByIdHttpResponseManager
{
    private readonly Dictionary<
        UpdateUserByIdResponseStatusCode,
        Func<UpdateUserByIdRequest, UpdateUserByIdResponse, UpdateUserByIdHttpResponse>
    > _dictionary;

    internal UpdateUserByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateUserByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateUserByIdResponseStatusCode.USER_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: UpdateUserByIdResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateUserByIdResponseStatusCode.UPLOAD_IMAGE_FAIL,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateUserByIdResponseStatusCode.ADDRESS_IS_NOT_CORRECT_FORMAT,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateUserByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateUserByIdRequest,
        UpdateUserByIdResponse,
        UpdateUserByIdHttpResponse
    > Resolve(UpdateUserByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
