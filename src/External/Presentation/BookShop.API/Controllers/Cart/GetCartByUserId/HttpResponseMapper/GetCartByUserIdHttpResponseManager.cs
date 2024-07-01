using System;
using System.Collections.Generic;
using BookShop.Application.Features.Address.GetAllAddresses;
using BookShop.Application.Features.Carts.GetCartByUserId;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Cart.GetCartByUserId.HttpResponseMapper;

/// <summary>
///     Mapper for GetCartByUserId feature
/// </summary>
public class GetCartByUserIdHttpResponseManager
{
    private readonly Dictionary<
        GetCartByUserIdResponseStatusCode,
        Func<GetCartByUserIdRequest, GetCartByUserIdResponse, GetCartByUserIdHttpResponse>
    > _dictionary;

    internal GetCartByUserIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetCartByUserIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetCartByUserIdResponseStatusCode.CART_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<
        GetCartByUserIdRequest,
        GetCartByUserIdResponse,
        GetCartByUserIdHttpResponse
    > Resolve(GetCartByUserIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
