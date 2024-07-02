using System;
using System.Collections.Generic;
using BookShop.Application.Features.Carts.ClearCart;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Cart.ClearCart.HttpResponseMapper;

/// <summary>
///     Mapper for ClearCart feature
/// </summary>
public class ClearCartHttpResponseManager
{
    private readonly Dictionary<
        ClearCartResponseStatusCode,
        Func<ClearCartRequest, ClearCartResponse, ClearCartHttpResponse>
    > _dictionary;

    internal ClearCartHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: ClearCartResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ClearCartResponseStatusCode.CART_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: ClearCartResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<ClearCartRequest, ClearCartResponse, ClearCartHttpResponse> Resolve(
        ClearCartResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
