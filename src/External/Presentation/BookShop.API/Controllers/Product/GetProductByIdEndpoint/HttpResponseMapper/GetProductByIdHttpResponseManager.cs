using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.GetProductById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.GetProductByIdEndpoint.HttpResponseMapper;

/// <summary>
///     Mapper for GetProductById feature
/// </summary>
public class GetProductByIdHttpResponseManager
{
    private readonly Dictionary<
        GetProductByIdResponseStatusCode,
        Func<GetProductByIdRequest, GetProductByIdResponse, GetProductByIdHttpResponse>
    > _dictionary;

    internal GetProductByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetProductByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetProductByIdResponseStatusCode.PRODUCT_IS_TEMPORARILY_REMOVED,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetProductByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND,
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
        GetProductByIdRequest,
        GetProductByIdResponse,
        GetProductByIdHttpResponse
    > Resolve(GetProductByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
