using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Orders.GetOrdersByUserId;

/// <summary>
///     GetOrdersByUserId Handler
/// </summary>
public class GetOrdersByUserIdHandler
    : IFeatureHandler<GetOrdersByUserIdRequest, GetOrdersByUserIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrdersByUserIdHandler(
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
    public async Task<GetOrdersByUserIdResponse> HandlerAsync(
        GetOrdersByUserIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claims json web token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Get order by orderId
        var orders =
            await _unitOfWork.OrderFeature.GetOrdersByUserIdRepository.FindOrdersByUserIdQueryAsync(
                userId: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetOrdersByUserIdResponse()
        {
            StatusCode = GetOrdersByUserIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetOrdersByUserIdResponse.Body()
            {
                TotalOrderCost = orders.Sum(order => order.TotalCost).ToString("0.000"),
                Orders = orders.Select(order => new GetOrdersByUserIdResponse.Body.Order()
                {
                    FullNameUser = $"{order.UserDetail.FirstName} {order.UserDetail.LastName}",
                    Address =
                        $"{order.Address.Ward}, {order.Address.District}, {order.Address.Province}",
                    TotalCost = order.TotalCost.ToString("0.000"),
                    OrderDate = order.OrderDate,
                    OrderDetails = order.OrderDetails.Select(
                        orderDetail => new GetOrdersByUserIdResponse.Body.Order.OrderDetail()
                        {
                            Id = orderDetail.Id,
                            Price = orderDetail.Product.Price.ToString("0.000"),
                            Quantity = orderDetail.Quantity,
                            Cost = (orderDetail.Product.Price * orderDetail.Quantity).ToString(
                                "0.000"
                            ),
                            Author = orderDetail.Product.Author,
                            ImageUrl = orderDetail.Product.ImageUrl,
                            NameProduct = orderDetail.Product.FullName,
                            OrderStatus = orderDetail.OrderStatus.FullName,
                        }
                    )
                })
            }
        };
    }
}
