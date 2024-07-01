using System;
using System.Collections.Generic;
using BookShop.Application.Features.Address.GetAllAddresses;
using BookShop.Application.Features.CartItems.AddItemToCart;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.CartItem.AddItemToCart.HttpResponseMapper;

/// <summary>
///     Mapper for AddItemToCart feature
/// </summary>
public class AddItemToCartHttpResponseManager
{
    private readonly Dictionary<
        AddItemToCartResponseStatusCode,
        Func<AddItemToCartRequest, AddItemToCartResponse, AddItemToCartHttpResponse>
    > _dictionary;

    internal AddItemToCartHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: AddItemToCartResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddItemToCartResponseStatusCode.PRODUCT_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddItemToCartResponseStatusCode.CART_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: AddItemToCartResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<AddItemToCartRequest, AddItemToCartResponse, AddItemToCartHttpResponse> Resolve(
        AddItemToCartResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
