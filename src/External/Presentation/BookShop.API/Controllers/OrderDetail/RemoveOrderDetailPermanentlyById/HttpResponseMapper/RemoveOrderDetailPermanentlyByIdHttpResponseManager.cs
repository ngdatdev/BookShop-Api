using System;
using System.Collections.Generic;
using BookShop.Application.Features.OrderDetails.RemoveOrderDetailPermanentlyById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.OrderDetail.RemoveOrderDetailPermanentlyById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveOrderDetailPermanentlyById feature
/// </summary>
public class RemoveOrderDetailPermanentlyByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveOrderDetailPermanentlyByIdResponseStatusCode,
        Func<
            RemoveOrderDetailPermanentlyByIdRequest,
            RemoveOrderDetailPermanentlyByIdResponse,
            RemoveOrderDetailPermanentlyByIdHttpResponse
        >
    > _dictionary;

    internal RemoveOrderDetailPermanentlyByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveOrderDetailPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderDetailPermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderDetailPermanentlyByIdResponseStatusCode.ORDER_DETAIL_IS_NOT_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveOrderDetailPermanentlyByIdResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveOrderDetailPermanentlyByIdRequest,
        RemoveOrderDetailPermanentlyByIdResponse,
        RemoveOrderDetailPermanentlyByIdHttpResponse
    > Resolve(RemoveOrderDetailPermanentlyByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
