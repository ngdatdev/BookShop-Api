using System;
using System.Collections.Generic;
using BookShop.Application.Features.Orders.RemoveOrderPermanentlyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Order.RemoveOrderPermanentlyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveOrderPermanentlyById feature
/// </summary>
public class RemoveOrderPermanentlyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveOrderPermanentlyByIdResponseStatusCode,
        Func<
            RemoveOrderPermanentlyByIdRequest,
            RemoveOrderPermanentlyByIdResponse,
            RemoveOrderPermanentlyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveOrderPermanentlyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveOrderPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderPermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderPermanentlyByIdResponseStatusCode.ORDER_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveOrderPermanentlyByIdRequest,
        RemoveOrderPermanentlyByIdResponse,
        RemoveOrderPermanentlyByIdHttpResponse
    > Resolve(RemoveOrderPermanentlyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
