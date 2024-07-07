using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Addresses.RemoveAddressPermanentlyRemovedById;

/// <summary>
///     RemoveAddressPermanentlyRemovedById Handler
/// </summary>
public class RemoveAddressPermanentlyRemovedByIdHandler
    : IFeatureHandler<
        RemoveAddressPermanentlyRemovedByIdRequest,
        RemoveAddressPermanentlyRemovedByIdResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveAddressPermanentlyRemovedByIdHandler(
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
    public async Task<RemoveAddressPermanentlyRemovedByIdResponse> HandlerAsync(
        RemoveAddressPermanentlyRemovedByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check address id is exist in database.
        var isOrderIdFound =
            await _unitOfWork.AddressFeature.RemoveAddressPermanentlyRemovedByIdRepository.IsAddressFoundByIdQueryAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Responds if address is not found.
        if (!isOrderIdFound)
        {
            return new()
            {
                StatusCode =
                    RemoveAddressPermanentlyRemovedByIdResponseStatusCode.ADDRESS_ID_IS_NOT_FOUND
            };
        }
        // Check address is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.AddressFeature.RemoveAddressPermanentlyRemovedByIdRepository.IsAddressTemporarilyRemovedByIdQueryAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Responds if addresss is temporarily removed.
        if (!isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveAddressPermanentlyRemovedByIdResponseStatusCode.ADDRESS_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Remove address temporarily command.
        var dbResult =
            await _unitOfWork.AddressFeature.RemoveAddressPermanentlyRemovedByIdRepository.RemoveAddressPermanentlyRemovedByIdCommandAsync(
                addressId: request.AddressId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode =
                    RemoveAddressPermanentlyRemovedByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveAddressPermanentlyRemovedByIdResponse()
        {
            StatusCode = RemoveAddressPermanentlyRemovedByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
