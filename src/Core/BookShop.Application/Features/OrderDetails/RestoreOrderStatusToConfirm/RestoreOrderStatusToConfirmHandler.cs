using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///     RestoreOrderStatusToConfirm Handler
/// </summary>
public class RestoreOrderStatusToConfirmHandler
    : IFeatureHandler<RestoreOrderStatusToConfirmRequest, RestoreOrderStatusToConfirmResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RestoreOrderStatusToConfirmHandler(
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
    public async Task<RestoreOrderStatusToConfirmResponse> HandlerAsync(
        RestoreOrderStatusToConfirmRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is order detail found by id.
        var isOrderDetailIdFound =
            await _unitOfWork.OrderDetailFeature.RestoreOrderStatusToConfirmRepository.IsOrderDetailFoundByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if order is not found.
        if (!isOrderDetailIdFound)
        {
            return new()
            {
                StatusCode =
                    RestoreOrderStatusToConfirmResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND
            };
        }
        // Check order is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.OrderDetailFeature.RestoreOrderStatusToConfirmRepository.IsOrderDetailTemporarilyRemovedByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if orders is temporarily removed.
        if (isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RestoreOrderStatusToConfirmResponseStatusCode.ORDER_DETAIL_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Get order status id by order id.
        var orderStatusValue =
            await _unitOfWork.OrderDetailFeature.RestoreOrderStatusToConfirmRepository.GetOrderStatusIdByOrderDetailIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Get next status.
        if (!orderStatusValue.Equals("Đã hủy"))
        {
            return new()
            {
                StatusCode =
                    RestoreOrderStatusToConfirmResponseStatusCode.ORDER_STATUS_IS_NOT_CANCELED
            };
        }

        // Get order status id by value.
        var orderStatusId =
            await _unitOfWork.OrderDetailFeature.RestoreOrderStatusToConfirmRepository.GetOrderStatusIdByValueQueryAsync(
                orderStatusValue: "Chờ xác nhận",
                cancellationToken
            );

        // Restore order temporarily command.
        var dbResult =
            await _unitOfWork.OrderDetailFeature.RestoreOrderStatusToConfirmRepository.RestoreOrderStatusToConfirmCommandAsync(
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
                StatusCode = RestoreOrderStatusToConfirmResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RestoreOrderStatusToConfirmResponse()
        {
            StatusCode = RestoreOrderStatusToConfirmResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
