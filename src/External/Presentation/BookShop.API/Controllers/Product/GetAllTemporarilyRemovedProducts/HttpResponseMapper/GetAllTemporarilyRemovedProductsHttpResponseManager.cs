using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.GetAllTemporarilyRemovedProducts;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.GetAllTemporarilyRemovedProducts.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllTemporarilyRemovedProducts feature
/// </summary>
public class GetAllTemporarilyRemovedProductsHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedProductsResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedProductsRequest,
            GetAllTemporarilyRemovedProductsResponse,
            GetAllTemporarilyRemovedProductsHttpResponse
        >
    > _dictionary;

    internal GetAllTemporarilyRemovedProductsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedProductsResponseStatusCode.OPERATION_SUCCESS,
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
        GetAllTemporarilyRemovedProductsRequest,
        GetAllTemporarilyRemovedProductsResponse,
        GetAllTemporarilyRemovedProductsHttpResponse
    > Resolve(GetAllTemporarilyRemovedProductsResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
