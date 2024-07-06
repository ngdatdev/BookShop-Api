using System;
using System.Collections.Generic;
using BookShop.Application.Features.Addresses.GetAllWardsByDistrictName;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Address.GetAllWardsByDistrictName.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class GetAllWardsByDistrictNameHttpResponseManager
{
    private readonly Dictionary<
        GetAllWardsByDistrictNameResponseStatusCode,
        Func<
            GetAllWardsByDistrictNameRequest,
            GetAllWardsByDistrictNameResponse,
            GetAllWardsByDistrictNameHttpResponse
        >
    > _dictionary;

    internal GetAllWardsByDistrictNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllWardsByDistrictNameResponseStatusCode.OPERATION_SUCCESS,
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
        GetAllWardsByDistrictNameRequest,
        GetAllWardsByDistrictNameResponse,
        GetAllWardsByDistrictNameHttpResponse
    > Resolve(GetAllWardsByDistrictNameResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
