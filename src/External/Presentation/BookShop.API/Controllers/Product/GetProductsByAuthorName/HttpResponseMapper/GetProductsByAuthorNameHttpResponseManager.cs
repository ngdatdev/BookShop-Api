using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.GetProductsByAuthorName;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.GetProductsByAuthorName.HttpResponseMapper;

/// <summary>
///     Mapper for GetProductsByAuthorName feature
/// </summary>
public class GetProductsByAuthorNameHttpResponseManager
{
    private readonly Dictionary<
        GetProductsByAuthorNameResponseStatusCode,
        Func<
            GetProductsByAuthorNameRequest,
            GetProductsByAuthorNameResponse,
            GetProductsByAuthorNameHttpResponse
        >
    > _dictionary;

    internal GetProductsByAuthorNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetProductsByAuthorNameResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetProductsByAuthorNameResponseStatusCode.AUTHOR_NAME_IS_NOT_FOUND,
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
        GetProductsByAuthorNameRequest,
        GetProductsByAuthorNameResponse,
        GetProductsByAuthorNameHttpResponse
    > Resolve(GetProductsByAuthorNameResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
