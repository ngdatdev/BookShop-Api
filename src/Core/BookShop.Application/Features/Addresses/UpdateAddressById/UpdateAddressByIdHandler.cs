using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Addresses.UpdateAddressById;

/// <summary>
///     UpdateAddressById Handler
/// </summary>
public class UpdateAddressByIdHandler
    : IFeatureHandler<UpdateAddressByIdRequest, UpdateAddressByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private IHttpContextAccessor _contextAccessor;

    public UpdateAddressByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = httpContextAccessor;
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
    public async Task<UpdateAddressByIdResponse> HandlerAsync(
        UpdateAddressByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find address by id.
        var foundAddress =
            await _unitOfWork.AddressFeature.UpdateAddressByIdRepository.FindAddressByIdQueryAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Responds if address is not found by id.
        if (Equals(objA: foundAddress, objB: default))
        {
            return new() { StatusCode = UpdateAddressByIdResponseStatusCode.ADDRESS_IS_NOT_FOUND };
        }

        // Is address temporarily removed by id.
        var isAddressTemporarilyRemoved =
            await _unitOfWork.AddressFeature.UpdateAddressByIdRepository.IsAddressTemporarilyRemovedByIdQueryAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Responds if address is temporarily removed.
        if (isAddressTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdateAddressByIdResponseStatusCode.ADDRESS_IS_TEMPORARILY_REMOVED
            };
        }

        // Get userId from claim.
        var userId = _contextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Init address
        var updateAddress = InitAddress(
            request: request,
            currentAddress: foundAddress,
            userId: Guid.Parse(input: userId)
        );

        // Update address by id command.
        var dbResult =
            await _unitOfWork.AddressFeature.UpdateAddressByIdRepository.UpdateAddressByIdCommandAsync(
                currentAddress: foundAddress,
                updateAddress: updateAddress,
                cancellationToken: cancellationToken
            );

        // Responds if database is fail.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = UpdateAddressByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new UpdateAddressByIdResponse()
        {
            StatusCode = UpdateAddressByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private Address InitAddress(
        UpdateAddressByIdRequest request,
        Address currentAddress,
        Guid userId
    )
    {
        return new()
        {
            Id = currentAddress.Id,
            Ward = request.Ward,
            District = request.District,
            Province = request.Province,
            CreatedAt = currentAddress.CreatedAt,
            CreatedBy = currentAddress.CreatedBy,
            RemovedAt = currentAddress.RemovedAt,
            RemovedBy = currentAddress.RemovedBy,
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = userId,
        };
    }
}
