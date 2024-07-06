using System;
using System.Collections.Generic;
using BookShop.Application.Features.Addresses.GetAllDistrictsByProvinceName;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Address.GetAllDistrictsByProvinceName.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class GetAllDistrictsByProvinceNameHttpResponseManager
{
    private readonly Dictionary<
        GetAllDistrictsByProvinceNameResponseStatusCode,
        Func<
            GetAllDistrictsByProvinceNameRequest,
            GetAllDistrictsByProvinceNameResponse,
            GetAllDistrictsByProvinceNameHttpResponse
        >
    > _dictionary;

    internal GetAllDistrictsByProvinceNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllDistrictsByProvinceNameResponseStatusCode.OPERATION_SUCCESS,
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
        GetAllDistrictsByProvinceNameRequest,
        GetAllDistrictsByProvinceNameResponse,
        GetAllDistrictsByProvinceNameHttpResponse
    > Resolve(GetAllDistrictsByProvinceNameResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
