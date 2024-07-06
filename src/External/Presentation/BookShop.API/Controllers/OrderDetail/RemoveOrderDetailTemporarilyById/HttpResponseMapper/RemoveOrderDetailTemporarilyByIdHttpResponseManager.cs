using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.RemoveOrderDetailTemporarilyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveOrderDetailTemporarilyById feature
/// </summary>
public class RemoveOrderDetailTemporarilyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveOrderDetailTemporarilyByIdResponseStatusCode,
        Func<
            RemoveOrderDetailTemporarilyByIdRequest,
            RemoveOrderDetailTemporarilyByIdResponse,
            RemoveOrderDetailTemporarilyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveOrderDetailTemporarilyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveOrderDetailTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderDetailTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderDetailTemporarilyByIdResponseStatusCode.ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderDetailTemporarilyByIdResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveOrderDetailTemporarilyByIdRequest,
        RemoveOrderDetailTemporarilyByIdResponse,
        RemoveOrderDetailTemporarilyByIdHttpResponse
    > Resolve(RemoveOrderDetailTemporarilyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
