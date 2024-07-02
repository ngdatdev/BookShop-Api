using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using BookShop.Data.Shared.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.CartItems.CreateOrder;

/// <summary>
///     CreateOrder Handler
/// </summary>
public class CreateOrderHandler : IFeatureHandler<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateOrderHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
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
    public async Task<CreateOrderResponse> HandlerAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claims json web token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Check list of product id.
        var isProductsFound =
            await _unitOfWork.OrderFeature.CreateOrderRepository.IsProductsFoundByIdQueryAsync(
                productIds: request.CartItems.Select(e => e.ProductId),
                cancellationToken: cancellationToken
            );

        // Responds if at least one product is not found.
        if (!isProductsFound)
        {
            return new() { StatusCode = CreateOrderResponseStatusCode.PRODUCTS_IS_NOT_FOUND, };
        }

        // Is one of products was temporarily removed
        var isProductsTemporarilyRemove =
            await _unitOfWork.OrderFeature.CreateOrderRepository.IsProductsTemporarilyRemovedQueryAsync(
                productIds: request.CartItems.Select(e => e.ProductId),
                cancellationToken: cancellationToken
            );

        // Responds if at least one product was temporarily removed.
        if (isProductsTemporarilyRemove)
        {
            return new()
            {
                StatusCode = CreateOrderResponseStatusCode.PRODUCTS_IS_TEMPORARILY_REMOVED
            };
        }

        // Find address id by name.
        var addressInfo = request.ShippingAddress.Split(separator: "<token/>");

        Guid addressId = default;
        try
        {
            addressId =
                await _unitOfWork.UserFeature.UpdateUserByIdRepository.FindAddressIdFoundByNameQueryAsync(
                    ward: addressInfo[0],
                    district: addressInfo[1],
                    province: addressInfo[2],
                    cancellationToken: cancellationToken
                );
        }
        catch
        {
            return new()
            {
                StatusCode = CreateOrderResponseStatusCode.ADDRESS_IS_NOT_CORRECT_FORMAT
            };
        }

        // Create address if it is not exist in database.
        if (Equals(objA: addressId, objB: Guid.Empty))
        {
            var dbAddressResult =
                await _unitOfWork.UserFeature.UpdateUserByIdRepository.CreateAddressCommandAsync(
                    address: InitAddress(
                        addressId: Guid.NewGuid(),
                        ward: addressInfo[0],
                        district: addressInfo[1],
                        province: addressInfo[2],
                        userId: Guid.Parse(input: userId)
                    ),
                    cancellationToken: cancellationToken
                );

            if (!dbAddressResult)
            {
                return new() { StatusCode = CreateOrderResponseStatusCode.DATABASE_OPERATION_FAIL };
            }
        }

        // Init OrderStatus
        var pendingConfirmationId = OrderStatusEnum.Get(
            status: OrderStatusEnum.OrderStatus.PendingConfirmation
        );

        // Init Orders Information
        var newOrder = InitOrder(
            request: request,
            addressId: addressId,
            userId: Guid.Parse(input: userId),
            orderStatusId: Guid.Parse(input: pendingConfirmationId)
        );

        // Create order.
        var dbResult = await _unitOfWork.OrderFeature.CreateOrderRepository.CreateOrderCommandAsync(
            order: newOrder,
            cancellationToken: cancellationToken
        );

        // Responds if database is fail.


        // Response successfully.
        return new CreateOrderResponse()
        {
            // Remove cartItem in cart.

            StatusCode = CreateOrderResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private Order InitOrder(
        CreateOrderRequest request,
        Guid userId,
        Guid addressId,
        Guid orderStatusId
    )
    {
        var orderId = Guid.NewGuid();

        return new Order()
        {
            Id = orderId,
            OrderDate = DateTime.UtcNow,
            ExpectedDate = DateTime.UtcNow.AddDays(7),
            TotalCost = request.CartItems.Sum(selector: cartItem =>
                cartItem.FinalPrice * cartItem.Quantity
            ),
            OrderStatusId = orderStatusId,
            AddressId = addressId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            OrderDetails = request.CartItems.Select(selector: cartItem => new OrderDetail()
            {
                Id = Guid.NewGuid(),
                Quantity = cartItem.Quantity,
                Cost = cartItem.FinalPrice,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
                RemovedAt = CommonConstant.MIN_DATE_TIME,
                RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                UpdatedAt = CommonConstant.MIN_DATE_TIME,
                UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                OrderId = orderId,
                ProductId = cartItem.ProductId,
            }),
        };
    }

    private Data.Shared.Entities.Address InitAddress(
        Guid addressId,
        string ward,
        string district,
        string province,
        Guid userId
    )
    {
        return new()
        {
            Id = addressId,
            Ward = ward,
            District = district,
            Province = province,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
        };
    }
}
