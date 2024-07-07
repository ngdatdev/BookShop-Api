using System;
using System.Collections.Generic;
using BookShop.Application.Features.Addresses.GetAllTemporarilyAddresses;
using Microsoft.AspNetCore.Http;

namespace BookShop.API.Controllers.Address.GetAllTemporarilyAddresses.HttpResponseMapper;

/// <summary>
///     Mapper for changing password feature
/// </summary>
public class GetAllTemporarilyAddressesHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyAddressesResponseStatusCode,
        Func<
            GetAllTemporarilyAddressesRequest,
            GetAllTemporarilyAddressesResponse,
            GetAllTemporarilyAddressesHttpResponse
        >
    > _dictionary;

    internal GetAllTemporarilyAddressesHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyAddressesResponseStatusCode.OPERATION_SUCCESS,
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
        GetAllTemporarilyAddressesRequest,
        GetAllTemporarilyAddressesResponse,
        GetAllTemporarilyAddressesHttpResponse
    > Resolve(GetAllTemporarilyAddressesResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
