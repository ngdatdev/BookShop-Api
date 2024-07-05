using System;
using System.Collections.Generic;
using BookShop.Application.Features.Roles.GetAllRolesTemporarilyRemoved;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Role.GetAllRolesTemporarilyRemoved.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllRolesTemporarilyRemoved feature
/// </summary>
public class GetAllRolesTemporarilyRemovedHttpResponseManager
{
    private readonly Dictionary<
        GetAllRolesTemporarilyRemovedResponseStatusCode,
        Func<
            GetAllRolesTemporarilyRemovedRequest,
            GetAllRolesTemporarilyRemovedResponse,
            GetAllRolesTemporarilyRemovedHttpResponse
        >
    > _dictionary;

    internal GetAllRolesTemporarilyRemovedHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllRolesTemporarilyRemovedResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );

        _dictionary.Add(
            key: GetAllRolesTemporarilyRemovedResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status500InternalServerError,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );
    }

    internal Func<
        GetAllRolesTemporarilyRemovedRequest,
        GetAllRolesTemporarilyRemovedResponse,
        GetAllRolesTemporarilyRemovedHttpResponse
    > Resolve(GetAllRolesTemporarilyRemovedResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
