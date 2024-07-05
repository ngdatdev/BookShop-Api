using System;
using System.Collections.Generic;
using BookShop.Application.Features.Roles.GetAllRoles;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Role.GetAllRoles.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllRoles feature
/// </summary>
public class GetAllRolesHttpResponseManager
{
    private readonly Dictionary<
        GetAllRolesResponseStatusCode,
        Func<GetAllRolesRequest, GetAllRolesResponse, GetAllRolesHttpResponse>
    > _dictionary;

    internal GetAllRolesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllRolesResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAllRolesResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<GetAllRolesRequest, GetAllRolesResponse, GetAllRolesHttpResponse> Resolve(
        GetAllRolesResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
