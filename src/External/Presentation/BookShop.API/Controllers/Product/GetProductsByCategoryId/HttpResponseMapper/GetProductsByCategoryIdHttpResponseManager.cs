using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.GetProductsByCategoryId;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.GetProductsByCategoryId.HttpResponseMapper;

/// <summary>
///     Mapper for GetProductsByCategoryId feature
/// </summary>
public class GetProductsByCategoryIdHttpResponseManager
{
    private readonly Dictionary<
        GetProductsByCategoryIdResponseStatusCode,
        Func<
            GetProductsByCategoryIdRequest,
            GetProductsByCategoryIdResponse,
            GetProductsByCategoryIdHttpResponse
        >
    > _dictionary;

    internal GetProductsByCategoryIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetProductsByCategoryIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetProductsByCategoryIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetProductsByCategoryIdResponseStatusCode.CATEGORY_ID_IS_NOT_CORRECT,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetProductsByCategoryIdResponseStatusCode.INPUT_VALIDATION_FAIL,
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
        GetProductsByCategoryIdRequest,
        GetProductsByCategoryIdResponse,
        GetProductsByCategoryIdHttpResponse
    > Resolve(GetProductsByCategoryIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
