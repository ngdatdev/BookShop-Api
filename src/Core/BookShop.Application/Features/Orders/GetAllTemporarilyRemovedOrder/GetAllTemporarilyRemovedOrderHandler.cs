using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Orders.GetAllTemporarilyRemovedOrder;

/// <summary>
///     GetAllTemporarilyRemovedOrder Handler
/// </summary>
public class GetAllTemporarilyRemovedOrderHandler
    : IFeatureHandler<GetAllTemporarilyRemovedOrderRequest, GetAllTemporarilyRemovedOrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllTemporarilyRemovedOrderHandler(
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
    public async Task<GetAllTemporarilyRemovedOrderResponse> HandlerAsync(
        GetAllTemporarilyRemovedOrderRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claims json web token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Get order by orderId
        var orders =
            await _unitOfWork.OrderFeature.GetAllTemporarilyRemovedOrderRepository.FindAllOrdersTemporarilyRemovedQueryAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Get total number orders.
        var countOrder =
            await _unitOfWork.OrderFeature.GetAllTemporarilyRemovedOrderRepository.CountTotalNumberOfTemporarilyRemovedOrders(
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllTemporarilyRemovedOrderResponse()
        {
            StatusCode = GetAllTemporarilyRemovedOrderResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetAllTemporarilyRemovedOrderResponse.Body()
            {
                Orders =
                    new Shared.Pagination.PaginationResponse<GetAllTemporarilyRemovedOrderResponse.Body.Order>()
                    {
                        Contents = orders.Select(
                            order => new GetAllTemporarilyRemovedOrderResponse.Body.Order()
                            {
                                FullNameUser =
                                    $"{order.UserDetail.FirstName} {order.UserDetail.LastName}",
                                Address =
                                    $"{order.Address.Ward}, {order.Address.District}, {order.Address.Province}",
                                TotalCost = order.TotalCost.ToString("0.000"),
                                OrderDate = order.OrderDate,
                                OrderDetails = order.OrderDetails.Select(
                                    orderDetail => new GetAllTemporarilyRemovedOrderResponse.Body.Order.OrderDetail()
                                    {
                                        Id = orderDetail.Id,
                                        Price = orderDetail.Product.Price.ToString("0.000"),
                                        Quantity = orderDetail.Quantity,
                                        Cost = (
                                            orderDetail.Product.Price * orderDetail.Quantity
                                        ).ToString("0.000"),
                                        Author = orderDetail.Product.Author,
                                        ImageUrl = orderDetail.Product.ImageUrl,
                                        NameProduct = orderDetail.Product.FullName,
                                        OrderStatus = orderDetail.OrderStatus.FullName,
                                    }
                                )
                            }
                        ),
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                        TotalPages = (int)Math.Ceiling((double)countOrder / request.PageSize)
                    }
            }
        };
    }
}
