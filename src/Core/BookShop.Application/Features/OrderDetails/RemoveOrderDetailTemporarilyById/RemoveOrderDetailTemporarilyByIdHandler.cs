using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailTemporarilyById;

/// <summary>
///     RemoveOrderDetailTemporarilyById Handler
/// </summary>
public class RemoveOrderDetailTemporarilyByIdHandler
    : IFeatureHandler<
        RemoveOrderDetailTemporarilyByIdRequest,
        RemoveOrderDetailTemporarilyByIdResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveOrderDetailTemporarilyByIdHandler(
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
    public async Task<RemoveOrderDetailTemporarilyByIdResponse> HandlerAsync(
        RemoveOrderDetailTemporarilyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check order id is exist in database.
        var isOrderIdFound =
            await _unitOfWork.OrderDetailFeature.RemoveOrderDetailTemporarilyByIdRepository.IsOrderDetailFoundByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if order is not found.
        if (!isOrderIdFound)
        {
            return new()
            {
                StatusCode =
                    RemoveOrderDetailTemporarilyByIdResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND
            };
        }
        // Check order is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.OrderDetailFeature.RemoveOrderDetailTemporarilyByIdRepository.IsOrderDetailTemporarilyRemovedByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if orders is temporarily removed.
        if (isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveOrderDetailTemporarilyByIdResponseStatusCode.ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Remove order temporarily command.
        var dbResult =
            await _unitOfWork.OrderDetailFeature.RemoveOrderDetailTemporarilyByIdRepository.RemoveOrderDetailTemporarilyByIdCommandAsync(
                orderDetailId: request.OrderDetailId,
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
                    RemoveOrderDetailTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveOrderDetailTemporarilyByIdResponse()
        {
            StatusCode = RemoveOrderDetailTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
