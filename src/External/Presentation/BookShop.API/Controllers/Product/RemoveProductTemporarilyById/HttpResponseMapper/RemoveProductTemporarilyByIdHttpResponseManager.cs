using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.RemoveProductTemporarilyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.RemoveProductTemporarilyByIdEndpoint.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveProductTemporarilyById feature
/// </summary>
public class RemoveProductTemporarilyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveProductTemporarilyByIdResponseStatusCode,
        Func<
            RemoveProductTemporarilyByIdRequest,
            RemoveProductTemporarilyByIdResponse,
            RemoveProductTemporarilyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveProductTemporarilyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveProductTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveProductTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveProductTemporarilyByIdResponseStatusCode.PRODUCT_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveProductTemporarilyByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        RemoveProductTemporarilyByIdRequest,
        RemoveProductTemporarilyByIdResponse,
        RemoveProductTemporarilyByIdHttpResponse
    > Resolve(RemoveProductTemporarilyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
