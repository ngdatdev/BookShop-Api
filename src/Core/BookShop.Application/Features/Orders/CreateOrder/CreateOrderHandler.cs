using System;
using System.Collections.Generic;
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

namespace BookShop.Application.Features.Orders.CreateOrder;

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
        var products =
            await _unitOfWork.OrderFeature.CreateOrderRepository.FindQuantityProductByIdQueryAsync(
                productIds: request.CartItems.Select(e => e.ProductId),
                cancellationToken: cancellationToken
            );

        // Responds if at least one product is not found.
        if (
            !Equals(
                objA: request.CartItems.Select(c => c.ProductId).Count(),
                objB: products.Count()
            )
        )
        {
            return new()
            {
                StatusCode = CreateOrderResponseStatusCode.PRODUCTS_IS_NOT_FOUND,
                NotFoundProductIds = request
                    .CartItems.Select(selector: c => c.ProductId)
                    .Except(second: products.Select(p => p.Id))
                    .ToList(),
            };
        }

        // Is one of products was temporarily removed.
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
                await _unitOfWork.OrderFeature.CreateOrderRepository.CreateAddressCommandAsync(
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

        // Match products with quantity
        List<(Guid ProductId, int Quantity)> cartItems = request
            .CartItems.Select(c => (c.ProductId, c.Quantity))
            .ToList();

        List<(Guid ProductId, decimal Price, int Discount, int CurrentQuantity)> prices = products
            .Select(p => (p.Id, p.Price, p.Discount, p.QuantityCurrent))
            .ToList();

        var matchedProducts = cartItems
            .Join(
                inner: prices,
                outerKeySelector: cartItem => cartItem.ProductId,
                innerKeySelector: price => price.ProductId,
                resultSelector: (cartItem, price) =>
                    new MatchedProduct()
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = price.Price,
                        Discount = price.Discount,
                        StockQuantity = price.CurrentQuantity,
                    }
            )
            .ToList();

        // Check is these quantities enough to order
        var productsWithInsufficientStock = matchedProducts
            .Where(predicate: mp => mp.Quantity > mp.StockQuantity)
            .Select(mp => mp.ProductId)
            .ToList();

        // Respond if one of quantity is better than stock quantity
        if (productsWithInsufficientStock.Any())
        {
            return new CreateOrderResponse()
            {
                StatusCode = CreateOrderResponseStatusCode.QUANTITY_IS_NOT_ENOUGH,
                NotFoundProductIds = productsWithInsufficientStock
            };
        }

        // Init OrderStatus
        var pendingConfirmationId = OrderStatusEnum.Get(
            status: OrderStatusEnum.OrderStatus.PendingConfirmation
        );

        // Init Orders Information
        var newOrder = InitOrder(
            products: matchedProducts,
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
        if (!dbResult)
        {
            return new() { StatusCode = CreateOrderResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new CreateOrderResponse()
        {
            // Remove cartItem in cart.

            StatusCode = CreateOrderResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private Order InitOrder(
        IEnumerable<MatchedProduct> products,
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
            TotalCost = products.Sum(selector: cartItem =>
                cartItem.Price * cartItem.Quantity * (1 - cartItem.Discount / 100.0m)
            ),
            AddressId = addressId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            OrderDetails = products
                .Select(selector: cartItem => new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    Quantity = cartItem.Quantity,
                    Cost = cartItem.Price * (1 - cartItem.Discount / 100.0m) * cartItem.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId,
                    RemovedAt = CommonConstant.MIN_DATE_TIME,
                    RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    UpdatedAt = CommonConstant.MIN_DATE_TIME,
                    UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    OrderId = orderId,
                    OrderStatusId = orderStatusId,
                    ProductId = cartItem.ProductId,
                })
                .ToList(),
        };
    }

    private Address InitAddress(
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

internal class MatchedProduct
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }

    public int StockQuantity { get; set; }
}
