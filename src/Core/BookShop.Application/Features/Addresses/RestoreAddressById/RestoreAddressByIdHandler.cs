using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Addresses.RestoreAddressById;

/// <summary>
///     RestoreAddressById Handler
/// </summary>
public class RestoreAddressByIdHandler
    : IFeatureHandler<RestoreAddressByIdRequest, RestoreAddressByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RestoreAddressByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<RestoreAddressByIdResponse> HandlerAsync(
        RestoreAddressByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check address id is exist in database.
        var isOrderIdFound =
            await _unitOfWork.AddressFeature.RestoreAddressByIdRepository.IsAddressFoundByIdQueryAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Responds if address is not found.
        if (!isOrderIdFound)
        {
            return new()
            {
                StatusCode = RestoreAddressByIdResponseStatusCode.ADDRESS_ID_IS_NOT_FOUND
            };
        }
        // Check address is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.AddressFeature.RestoreAddressByIdRepository.IsAddressTemporarilyRemovedByIdQueryAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Responds if addresss is temporarily removed.
        if (!isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RestoreAddressByIdResponseStatusCode.ADDRESS_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Remove address temporarily command.
        var dbResult =
            await _unitOfWork.AddressFeature.RestoreAddressByIdRepository.RestoreAddressByIdCommandAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RestoreAddressByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RestoreAddressByIdResponse()
        {
            StatusCode = RestoreAddressByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
