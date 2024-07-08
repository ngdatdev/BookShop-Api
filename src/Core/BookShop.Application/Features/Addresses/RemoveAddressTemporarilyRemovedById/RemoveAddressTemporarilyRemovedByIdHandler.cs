using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Addresses.RemoveAddressTemporarilyRemovedById;

/// <summary>
///     RemoveAddressTemporarilyRemovedById Handler
/// </summary>
public class RemoveAddressTemporarilyRemovedByIdHandler
    : IFeatureHandler<
        RemoveAddressTemporarilyRemovedByIdRequest,
        RemoveAddressTemporarilyRemovedByIdResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveAddressTemporarilyRemovedByIdHandler(
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
    public async Task<RemoveAddressTemporarilyRemovedByIdResponse> HandlerAsync(
        RemoveAddressTemporarilyRemovedByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check address id is exist in database.
        var isOrderIdFound =
            await _unitOfWork.AddressFeature.RemoveAddressTemporarilyRemovedByIdRepository.IsAddressFoundByIdQueryAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Respond if address is not found.
        if (!isOrderIdFound)
        {
            return new()
            {
                StatusCode =
                    RemoveAddressTemporarilyRemovedByIdResponseStatusCode.ADDRESS_ID_IS_NOT_FOUND
            };
        }
        // Check address is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.AddressFeature.RemoveAddressTemporarilyRemovedByIdRepository.IsAddressTemporarilyRemovedByIdQueryAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Respond if addresss is temporarily removed.
        if (isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveAddressTemporarilyRemovedByIdResponseStatusCode.ADDRESS_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Remove address temporarily command.
        var dbResult =
            await _unitOfWork.AddressFeature.RemoveAddressTemporarilyRemovedByIdRepository.RemoveAddressTemporarilyRemovedByIdCommandAsync(
                addressId: request.AddressId,
                removedAt: DateTime.UtcNow,
                removedBy: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode =
                    RemoveAddressTemporarilyRemovedByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveAddressTemporarilyRemovedByIdResponse()
        {
            StatusCode = RemoveAddressTemporarilyRemovedByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
