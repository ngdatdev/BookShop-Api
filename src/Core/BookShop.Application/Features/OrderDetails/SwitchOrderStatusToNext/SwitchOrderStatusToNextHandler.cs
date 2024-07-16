using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///     SwitchOrderStatusToNext Handler
/// </summary>
public class SwitchOrderStatusToNextHandler
    : IFeatureHandler<SwitchOrderStatusToNextRequest, SwitchOrderStatusToNextResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private static readonly Dictionary<string, string> NextStatus = new Dictionary<string, string>
    {
        { "Chờ xác nhận", "Đã xác nhận" },
        { "Đã xác nhận", "Đang vận chuyển" },
        { "Đang vận chuyển", "Đã giao hàng" },
        { "Đã giao hàng", "" }
    };

    public SwitchOrderStatusToNextHandler(
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
    public async Task<SwitchOrderStatusToNextResponse> HandlerAsync(
        SwitchOrderStatusToNextRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is order detail found by id.
        var isOrderDetailIdFound =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToNextByIdRepository.IsOrderDetailFoundByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if order is not found.
        if (!isOrderDetailIdFound)
        {
            return new()
            {
                StatusCode = SwitchOrderStatusToNextResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND
            };
        }
        // Check order is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToNextByIdRepository.IsOrderDetailTemporarilyRemovedByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if orders is temporarily removed.
        if (isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    SwitchOrderStatusToNextResponseStatusCode.ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Get order status id by order id.
        var orderStatusValue =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToNextByIdRepository.GetOrderStatusIdByOrderDetailIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Get next status.
        var nextStatus = NextStatus[orderStatusValue];

        if (nextStatus.IsNullOrEmpty())
        {
            return new()
            {
                StatusCode = SwitchOrderStatusToNextResponseStatusCode.ORDER_STATUS_IS_END
            };
        }

        // Get order status id by value.
        var orderStatusId =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToNextByIdRepository.GetOrderStatusIdByValueQueryAsync(
                orderStatusValue: nextStatus,
                cancellationToken
            );

        // Remove order temporarily command.
        var dbResult =
            await _unitOfWork.OrderDetailFeature.SwitchOrderStatusToNextByIdRepository.SwitchOrderStatusToNextCommandAsync(
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
                StatusCode = SwitchOrderStatusToNextResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new SwitchOrderStatusToNextResponse()
        {
            StatusCode = SwitchOrderStatusToNextResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
