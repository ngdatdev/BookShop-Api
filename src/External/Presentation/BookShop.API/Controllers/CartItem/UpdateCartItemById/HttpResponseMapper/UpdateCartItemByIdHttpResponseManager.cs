using System;
using System.Collections.Generic;
using BookShop.Application.Features.CartItems.UpdateCartItemById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.CartItem.UpdateCartItemById.HttpResponseMapper;

/// <summary>
///     Mapper for UpdateCartItemById feature
/// </summary>
public class UpdateCartItemByIdHttpResponseManager
{
    private readonly Dictionary<
        UpdateCartItemByIdResponseStatusCode,
        Func<UpdateCartItemByIdRequest, UpdateCartItemByIdResponse, UpdateCartItemByIdHttpResponse>
    > _dictionary;

    internal UpdateCartItemByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateCartItemByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateCartItemByIdResponseStatusCode.CART_ITEM_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateCartItemByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateCartItemByIdResponseStatusCode.QUANTITY_IS_NOT_ENOUGH,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateCartItemByIdRequest,
        UpdateCartItemByIdResponse,
        UpdateCartItemByIdHttpResponse
    > Resolve(UpdateCartItemByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
