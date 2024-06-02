using System;
using System.Collections.Generic;
using BookShop.Application.ResponseEntity;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.HttpResponseMapper.ErrorApiResponse;

/// <summary>
///     Manages the mapping between app
///     response and http response for error api.
/// </summary>
internal sealed class ErrorHttpResponseManager
{
    private readonly Dictionary<ResponseAppCode, Func<ErrorHttpResponse>> _dictionary;

    internal ErrorHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ResponseAppCode.INPUT_VALIDATION_FAIL,
            value: () =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    ErrorMessages = ["Inputs is not valid"]
                }
        );

        _dictionary.Add(
            key: ResponseAppCode.USERNAME_IS_NOT_FOUND,
            value: () =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    ErrorMessages = ["Username is not found"]
                }
        );
        _dictionary.Add(
            key: ResponseAppCode.PASSWORD_IS_NOT_CORRECT,
            value: () =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    ErrorMessages = ["Password is not correct"]
                }
        );
    }

    internal Func<ErrorHttpResponse> Resolve(ResponseAppCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
