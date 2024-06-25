using System;
using System.Collections.Generic;
using BookShop.Application.Features.Address.GetAllAddresses;
using BookShop.Application.Features.Product.GetAllProducts;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.GetAllProducts.HttpResponseMapper;

/// <summary>
///     Mapper for login feature
/// </summary>
public class GetAllProductsHttpResponseManager
{
    private readonly Dictionary<
        GetAllProductsResponseStatusCode,
        Func<GetAllProductsRequest, GetAllProductsResponse, GetAllProductsHttpResponse>
    > _dictionary;

    internal GetAllProductsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllProductsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<
        GetAllProductsRequest,
        GetAllProductsResponse,
        GetAllProductsHttpResponse
    > Resolve(GetAllProductsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
