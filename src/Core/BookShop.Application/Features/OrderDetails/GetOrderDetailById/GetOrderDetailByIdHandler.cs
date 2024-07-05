using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.OrderDetails.GetOrderDetailById;

/// <summary>
///     GetOrderDetailById Handler
/// </summary>
public class GetOrderDetailByIdHandler
    : IFeatureHandler<GetOrderDetailByIdRequest, GetOrderDetailByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderDetailByIdHandler(
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
    /// </returns>
    public async Task<GetOrderDetailByIdResponse> HandlerAsync(
        GetOrderDetailByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claim.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Is order detail found by user id and order detail id.
        var isOrderDetailFoundById =
            await _unitOfWork.OrderDetailFeature.GetOrderDetailByIdRepository.IsOrderDetailFoundByUserIdAndOrderDetailIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                userId: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if order detail is not found by user id and order detail id.
        if (!isOrderDetailFoundById)
        {
            return new()
            {
                StatusCode = GetOrderDetailByIdResponseStatusCode.ORDER_DETAIL_IS_NOT_FOUND,
            };
        }

        // Is order detail temporarily removed.
        var isOrderDetailTemporarilyRemoved =
            await _unitOfWork.OrderDetailFeature.GetOrderDetailByIdRepository.IsOrderDetailTemporarilyRemovedById(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        if (isOrderDetailTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    GetOrderDetailByIdResponseStatusCode.ORDER_DETAIL_IS_TEMPORARILY_REMOVED
            };
        }

        // Get order detail by userid and orderDetailId.
        var foundOrder =
            await _unitOfWork.OrderDetailFeature.GetOrderDetailByIdRepository.FindOrderDetailByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                userId: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetOrderDetailByIdResponse()
        {
            StatusCode = GetOrderDetailByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetOrderDetailByIdResponse.Body()
            {
                FullNameUser =
                    $"{foundOrder.Order.UserDetail.FirstName} {foundOrder.Order.UserDetail.LastName}",
                Address =
                    $"{foundOrder.Order.Address.Ward}, {foundOrder.Order.Address.District}, {foundOrder.Order.Address.Province}",
                TotalCost = foundOrder.Order.TotalCost.ToString("0.000"),
                OrderDate = foundOrder.Order.OrderDate,
                OrderDetailInformation = new GetOrderDetailByIdResponse.Body.OrderDetail()
                {
                    Id = request.OrderDetailId,
                    Price = foundOrder.Product.Price.ToString("0.000"),
                    Quantity = foundOrder.Quantity,
                    Cost = (foundOrder.Product.Price * foundOrder.Quantity).ToString("0.000"),
                    Author = foundOrder.Product.Author,
                    ImageUrl = foundOrder.Product.ImageUrl,
                    NameProduct = foundOrder.Product.FullName,
                    OrderStatus = foundOrder.OrderStatus.FullName,
                }
            }
        };
    }
}
