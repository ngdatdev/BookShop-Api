using System;
using System.Collections.Generic;
using BookShop.Application.Features.Auth.LoginByGoogle;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Auth.LoginByGoogle.HttpResponseMapper;

/// <summary>
///     Manager for LoginByGoogle feature
/// </summary>
public class LoginByGoogleHttpResponseManager
{
    private readonly Dictionary<
        LoginByGoogleResponseStatusCode,
        Func<LoginByGoogleRequest, LoginByGoogleResponse, LoginByGoogleHttpResponse>
    > _dictionary;

    internal LoginByGoogleHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: LoginByGoogleResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: LoginByGoogleResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );

        _dictionary.Add(
            key: LoginByGoogleResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
       

        _dictionary.Add(
            key: LoginByGoogleResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status417ExpectationFailed,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: LoginByGoogleResponseStatusCode.UNAUTHORIZE,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<LoginByGoogleRequest, LoginByGoogleResponse, LoginByGoogleHttpResponse> Resolve(
        LoginByGoogleResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
