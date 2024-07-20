using System;
using System.Collections.Generic;
using BookShop.Application.Features.Product.SearchProductsByKeyword;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Product.SearchProductsByKeyword.HttpResponseMapper;

/// <summary>
///     Mapper for SearchProductsByKeyword feature
/// </summary>
public class SearchProductsByKeywordHttpResponseManager
{
    private readonly Dictionary<
        SearchProductsByKeywordResponseStatusCode,
        Func<
            SearchProductsByKeywordRequest,
            SearchProductsByKeywordResponse,
            SearchProductsByKeywordHttpResponse
        >
    > _dictionary;

    internal SearchProductsByKeywordHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: SearchProductsByKeywordResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: SearchProductsByKeywordResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<
        SearchProductsByKeywordRequest,
        SearchProductsByKeywordResponse,
        SearchProductsByKeywordHttpResponse
    > Resolve(SearchProductsByKeywordResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
