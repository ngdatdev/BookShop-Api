using System;
using System.Collections.Generic;
using BookShop.Application.Features.HelloWorld;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Endpoints.HelloWorld.HttpResponseMapper;

/// <summary>
///     Mapper for hello world feature
/// </summary>
public class HelloWorldHttpResponseManager
{
    private readonly Dictionary<
        HelloWorldResponseStatusCode,
        Func<HelloWorldRequest, HelloWorldResponse, HelloWorldHttpResponse>
    > _dictionary;

    internal HelloWorldHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: HelloWorldResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: HelloWorldResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<HelloWorldRequest, HelloWorldResponse, HelloWorldHttpResponse> Resolve(
        HelloWorldResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
