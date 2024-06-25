using System;
using System.Collections.Generic;
using BookShop.Application.Features.Address.GetAllAddresses;
using BookShop.Application.Features.Users.GetAllUsers;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.User.GetAllUsers.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllUsers feature
/// </summary>
public class GetAllUsersHttpResponseManager
{
    private readonly Dictionary<
        GetAllUsersResponseStatusCode,
        Func<GetAllUsersRequest, GetAllUsersResponse, GetAllUsersHttpResponse>
    > _dictionary;

    internal GetAllUsersHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllUsersResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                    Body = response.ResponseBody
                }
        );
    }

    internal Func<GetAllUsersRequest, GetAllUsersResponse, GetAllUsersHttpResponse> Resolve(
        GetAllUsersResponseStatusCode statusCode
    )
    {
        return _dictionary[statusCode];
    }
}
