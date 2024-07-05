using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.OrderDetails.GetOrderDetailsByOrderStatusId;

/// <summary>
///     GetOrderDetailsByOrderStatusId Handler
/// </summary>
public class GetOrderDetailsByOrderStatusIdHandler
    : IFeatureHandler<GetOrderDetailsByOrderStatusIdRequest, GetOrderDetailsByOrderStatusIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderDetailsByOrderStatusIdHandler(
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
    public async Task<GetOrderDetailsByOrderStatusIdResponse> HandlerAsync(
        GetOrderDetailsByOrderStatusIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claim.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Is order status id found.
        var isOrderStatusFound =
            await _unitOfWork.OrderDetailFeature.GetOrderDetailsByOrderStatusIdRepository.IsOrderStatusFoundById(
                orderStatusId: request.OrderStatusId,
                cancellationToken: cancellationToken
            );

        // Responds if order status id is not found.
        if (!isOrderStatusFound)
        {
            return new()
            {
                StatusCode =
                    GetOrderDetailsByOrderStatusIdResponseStatusCode.ORDER_STATUS_IS_NOT_FOUND
            };
        }

        // Get order details by userid and order status id.
        var orderDetails =
            await _unitOfWork.OrderDetailFeature.GetOrderDetailsByOrderStatusIdRepository.FindOrderDetailsByStatusIdAndUserIdQueryAsync(
                orderStatusId: request.OrderStatusId,
                userId: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetOrderDetailsByOrderStatusIdResponse()
        {
            StatusCode = GetOrderDetailsByOrderStatusIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetOrderDetailsByOrderStatusIdResponse.Body()
            {
                OrderDetails = orderDetails.Select(
                    orderDetail => new GetOrderDetailsByOrderStatusIdResponse.Body.OrderDetail()
                    {
                        Id = orderDetail.Id,
                        Price = orderDetail.Product.Price.ToString("0.000"),
                        Quantity = orderDetail.Quantity,
                        Cost = (orderDetail.Product.Price * orderDetail.Quantity).ToString("0.000"),
                        Author = orderDetail.Product.Author,
                        ImageUrl = orderDetail.Product.ImageUrl,
                        NameProduct = orderDetail.Product.FullName,
                        OrderStatus = orderDetail.OrderStatus.FullName,
                    }
                )
            }
        };
    }
}
