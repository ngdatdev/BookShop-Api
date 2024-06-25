using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.RemoveProductPermanentlyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.RemoveProductPermanentlyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveProductPermanentlyById feature
/// </summary>
public class RemoveProductPermanentlyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveProductPermanentlyByIdResponseStatusCode,
        Func<
            RemoveProductPermanentlyByIdRequest,
            RemoveProductPermanentlyByIdResponse,
            RemoveProductPermanentlyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveProductPermanentlyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveProductPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveProductPermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveProductPermanentlyByIdResponseStatusCode.PRODUCT_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveProductPermanentlyByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode()
                }
        );
    }

    internal Func<
        RemoveProductPermanentlyByIdRequest,
        RemoveProductPermanentlyByIdResponse,
        RemoveProductPermanentlyByIdHttpResponse
    > Resolve(RemoveProductPermanentlyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
