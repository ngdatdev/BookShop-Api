using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.CreateProduct;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.CreateProductEndpoint.HttpResponseMapper;

/// <summary>
///     Mapper for login feature
/// </summary>
public class CreateProductHttpResponseManager
{
    private readonly Dictionary<
        CreateProductResponseStatusCode,
        Func<CreateProductRequest, CreateProductResponse, CreateProductHttpResponse>
    > _dictionary;

    internal CreateProductHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateProductResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateProductResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateProductResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateProductResponseStatusCode.MAIN_IMAGE_FILE_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
            key: CreateProductResponseStatusCode.SUB_IMAGE_FILE_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<CreateProductRequest, CreateProductResponse, CreateProductHttpResponse> Resolve(
        CreateProductResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
