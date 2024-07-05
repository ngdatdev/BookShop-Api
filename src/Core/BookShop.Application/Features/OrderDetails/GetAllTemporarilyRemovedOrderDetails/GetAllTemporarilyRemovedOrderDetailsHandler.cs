using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.OrderDetails.GetAllTemporarilyRemovedOrderDetails;

/// <summary>
///     GetAllTemporarilyRemovedOrderDetails Handler
/// </summary>
public class GetAllTemporarilyRemovedOrderDetailsHandler
    : IFeatureHandler<
        GetAllTemporarilyRemovedOrderDetailsRequest,
        GetAllTemporarilyRemovedOrderDetailsResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllTemporarilyRemovedOrderDetailsHandler(
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
    public async Task<GetAllTemporarilyRemovedOrderDetailsResponse> HandlerAsync(
        GetAllTemporarilyRemovedOrderDetailsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claims json web token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Get order by orderId
        var orderDetails =
            await _unitOfWork.OrderDetailFeature.GetAllTemporarilyRemovedOrderDetailsRepository.FindAllTemporarilyRemovedOrderDetailsQueryAsync(
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllTemporarilyRemovedOrderDetailsResponse()
        {
            StatusCode = GetAllTemporarilyRemovedOrderDetailsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new GetAllTemporarilyRemovedOrderDetailsResponse.Body()
            {
                OrderDetails =
                    new Shared.Pagination.PaginationResponse<GetAllTemporarilyRemovedOrderDetailsResponse.Body.OrderDetail>()
                    {
                        Contents = orderDetails.Select(
                            orderDetail => new GetAllTemporarilyRemovedOrderDetailsResponse.Body.OrderDetail()
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
                                OrderStatus = orderDetail.OrderStatus.FullName
                            }
                        ),
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                    }
            }
        };
    }
}
