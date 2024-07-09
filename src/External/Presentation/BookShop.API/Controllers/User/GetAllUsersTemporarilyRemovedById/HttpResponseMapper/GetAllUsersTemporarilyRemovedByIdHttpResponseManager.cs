using System;
using System.Collections.Generic;
using BookShop.Application.Features.Users.GetAllUsersTemporarilyRemovedById;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.User.GetAllUsersTemporarilyRemovedById.HttpResponseMapper;

/// <summary>
///     Mapper for GetAllUsersTemporarilyRemovedById feature
/// </summary>
public class GetAllUsersTemporarilyRemovedByIdHttpResponseManager
{
    private readonly Dictionary<
        GetAllUsersTemporarilyRemovedByIdResponseStatusCode,
        Func<
            GetAllUsersTemporarilyRemovedByIdRequest,
            GetAllUsersTemporarilyRemovedByIdResponse,
            GetAllUsersTemporarilyRemovedByIdHttpResponse
        >
    > _dictionary;

    internal GetAllUsersTemporarilyRemovedByIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllUsersTemporarilyRemovedByIdResponseStatusCode.OPERATION_SUCCESS,
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
        GetAllUsersTemporarilyRemovedByIdRequest,
        GetAllUsersTemporarilyRemovedByIdResponse,
        GetAllUsersTemporarilyRemovedByIdHttpResponse
    > Resolve(GetAllUsersTemporarilyRemovedByIdResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
