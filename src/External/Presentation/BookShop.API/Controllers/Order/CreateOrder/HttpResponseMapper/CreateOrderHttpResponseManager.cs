using System;
using System.Collections.Generic;
using BookShop.Application.Features.Orders.CreateOrder;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Order.CreateOrder.HttpResponseMapper;

/// <summary>
///     Mapper for CreateOrder feature
/// </summary>
public class CreateOrderHttpResponseManager
{
    private readonly Dictionary<
        CreateOrderResponseStatusCode,
        Func<CreateOrderRequest, CreateOrderResponse, CreateOrderHttpResponse>
    > _dictionary;

    internal CreateOrderHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateOrderResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateOrderResponseStatusCode.PRODUCTS_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateOrderResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateOrderResponseStatusCode.QUANTITY_IS_NOT_ENOUGH,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateOrderResponseStatusCode.CART_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateOrderResponseStatusCode.PRODUCTS_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<CreateOrderRequest, CreateOrderResponse, CreateOrderHttpResponse> Resolve(
        CreateOrderResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
