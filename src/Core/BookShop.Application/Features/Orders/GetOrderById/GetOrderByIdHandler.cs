using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Orders.GetOrderById;

/// <summary>
///     GetOrderById Handler
/// </summary>
public class GetOrderByIdHandler : IFeatureHandler<GetOrderByIdRequest, GetOrderByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
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
    public async Task<GetOrderByIdResponse> HandlerAsync(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claims json web token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Get order by orderId
        var order = await _unitOfWork.OrderFeature.GetOrderByIdRepository.FindOrderByIdQueryAsync(
            orderId: request.OrderId,
            cancellationToken: cancellationToken
        );

        // Responds if order is not found.
        if (Equals(objA: order, objB: default))
        {
            return new() { StatusCode = GetOrderByIdResponseStatusCode.ORDER_ID_IS_NOT_FOUND };
        }

        // Response successfully.
        return new GetOrderByIdResponse()
        {
            StatusCode = GetOrderByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetOrderByIdResponse.Body()
            {
                FullNameUser = $"{order.UserDetail.FirstName} {order.UserDetail.LastName}",
                Address =
                    $"{order.Address.Ward}, {order.Address.District}, {order.Address.Province}",
                TotalCost = order.TotalCost.ToString("0.000"),
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(
                    orderDetail => new GetOrderByIdResponse.Body.OrderDetail()
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
