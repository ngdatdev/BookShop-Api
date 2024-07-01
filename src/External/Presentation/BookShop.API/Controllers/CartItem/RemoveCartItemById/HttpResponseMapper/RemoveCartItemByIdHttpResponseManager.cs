using System;
using System.Collections.Generic;
using BookShop.Application.Features.CartItems.RemoveCartItemById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.CartItem.RemoveCartItemById.HttpResponseMapper;

/// <summary>
///     Mapper for RemoveCartItemById feature
/// </summary>
public class RemoveCartItemByIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveCartItemByIdResponseStatusCode,
        Func<RemoveCartItemByIdRequest, RemoveCartItemByIdResponse, RemoveCartItemByIdHttpResponse>
    > _dictionary;

    internal RemoveCartItemByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveCartItemByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveCartItemByIdResponseStatusCode.CART_ITEM_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: RemoveCartItemByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        RemoveCartItemByIdRequest,
        RemoveCartItemByIdResponse,
        RemoveCartItemByIdHttpResponse
    > Resolve(RemoveCartItemByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
