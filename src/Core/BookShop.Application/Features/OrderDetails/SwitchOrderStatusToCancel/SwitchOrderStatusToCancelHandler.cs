using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///     SwitchOrderStatusToCancel Handler
/// </summary>
public class SwitchOrderStatusToCancelHandler
    : IFeatureHandler<SwitchOrderStatusToCancelRequest, SwitchOrderStatusToCancelResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SwitchOrderStatusToCancelHandler(
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
    public async Task<SwitchOrderStatusToCancelResponse> HandlerAsync(
        SwitchOrderStatusToCancelRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is order detail found by id.
        var isOrderDetailIdFound =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToCancelRepository.IsOrderDetailFoundByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if order is not found.
        if (!isOrderDetailIdFound)
        {
            return new()
            {
                StatusCode =
                    SwitchOrderStatusToCancelResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND
            };
        }
        // Check order is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToCancelRepository.IsOrderDetailTemporarilyRemovedByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if orders is temporarily removed.
        if (isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    SwitchOrderStatusToCancelResponseStatusCode.ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Get order status id by order id.
        var orderStatusValue =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToCancelRepository.GetOrderStatusIdByOrderDetailIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Get next status.
        if (orderStatusValue.Equals("Đang vận chuyển") || orderStatusValue.Equals("Đã giao hàng"))
        {
            return new()
            {
                StatusCode =
                    SwitchOrderStatusToCancelResponseStatusCode.ORDER_STATUS_IS_CAN_NOT_CANCEL
            };
        }

        // Get order status id by value.
        var orderStatusId =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToCancelRepository.GetOrderStatusIdByValueQueryAsync(
                orderStatusValue: "Đang vận chuyển",
                cancellationToken
            );

        // Remove order temporarily command.
        var dbResult =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToCancelRepository.SwitchOrderStatusToCancelCommandAsync(
                orderDetailId: request.OrderDetailId,
                newOrderStatusId: orderStatusId,
                updatedAt: DateTime.UtcNow,
                updatedBy: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = SwitchOrderStatusToCancelResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new SwitchOrderStatusToCancelResponse()
        {
            StatusCode = SwitchOrderStatusToCancelResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
