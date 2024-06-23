using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.UpdateProductById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.UpdateProductByIdEndpoint.HttpResponseMapper;

/// <summary>
///     Mapper for UpdateProductById feature
/// </summary>
public class UpdateProductByIdHttpResponseManager
{
    private readonly Dictionary<
        UpdateProductByIdResponseStatusCode,
        Func<UpdateProductByIdRequest, UpdateProductByIdResponse, UpdateProductByIdHttpResponse>
    > _dictionary;

    internal UpdateProductByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateProductByIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateProductByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateProductByIdResponseStatusCode.MAIN_IMAGE_FILE_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateProductByIdResponseStatusCode.SUB_IMAGE_FILE_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateProductByIdResponseStatusCode.CATEGORY_ID_IS_NOT_VALID,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: UpdateProductByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status404NotFound,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        UpdateProductByIdRequest,
        UpdateProductByIdResponse,
        UpdateProductByIdHttpResponse
    > Resolve(UpdateProductByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
