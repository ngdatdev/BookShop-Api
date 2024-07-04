using System;
using System.Collections.Generic;
using BookShop.Application.Features.Orders.RemoveOrderTemporarilyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Order.RemoveOrderTemporarilyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveOrderTemporarilyById feature
/// </summary>
public class RemoveOrderTemporarilyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveOrderTemporarilyByIdResponseStatusCode,
        Func<
            RemoveOrderTemporarilyByIdRequest,
            RemoveOrderTemporarilyByIdResponse,
            RemoveOrderTemporarilyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveOrderTemporarilyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveOrderTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveOrderTemporarilyByIdRequest,
        RemoveOrderTemporarilyByIdResponse,
        RemoveOrderTemporarilyByIdHttpResponse
    > Resolve(RemoveOrderTemporarilyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
