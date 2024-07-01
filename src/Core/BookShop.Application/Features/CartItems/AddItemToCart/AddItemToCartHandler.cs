using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.CartItems.AddItemToCart;

/// <summary>
///     AddItemToCart Handler
/// </summary>
public class AddItemToCartHandler : IFeatureHandler<AddItemToCartRequest, AddItemToCartResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddItemToCartHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
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
    public async Task<AddItemToCartResponse> HandlerAsync(
        AddItemToCartRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claims json web token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Find cartId by userId.
        var cartId =
            await _unitOfWork.CartItemFeature.AddItemToCartRepository.FindCartIdByUserIdQueryAsync(
                userId: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if cart is not found.
        if (Equals(objA: cartId, objB: Guid.Empty))
        {
            return new() { StatusCode = AddItemToCartResponseStatusCode.CART_ID_IS_NOT_FOUND };
        }

        // Find product by id is exist.
        var foundProduct =
            await _unitOfWork.CartItemFeature.AddItemToCartRepository.FindProductByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if product is not found.
        if (Equals(objA: foundProduct, objB: default))
        {
            return new() { StatusCode = AddItemToCartResponseStatusCode.PRODUCT_IS_NOT_FOUND, };
        }

        // Responds if quantity of request is higher than current quantity.
        if (foundProduct.QuantityCurrent < request.Quantity)
        {
            return new() { StatusCode = AddItemToCartResponseStatusCode.QUANTITY_IS_NOT_ENOUGH, };
        }

        // Find cart item in current cart.
        var cartItem =
            await _unitOfWork.CartItemFeature.AddItemToCartRepository.FindCartItemByProductIdAndCartIdQueryAsync(
                productId: request.ProductId,
                cartId: cartId,
                cancellationToken: cancellationToken
            );

        if (!Equals(objA: cartItem, objB: default))
        {
            var updateQuantityProduct =
                await _unitOfWork.CartItemFeature.AddItemToCartRepository.UpdateCartItemCommandAsync(
                    cartItemId: cartItem.Id,
                    quantity: request.Quantity + cartItem.Quantity,
                    cancellationToken: cancellationToken
                );

            if (!updateQuantityProduct)
            {
                return new()
                {
                    StatusCode = AddItemToCartResponseStatusCode.DATABASE_OPERATION_FAIL,
                };
            }

            return new() { StatusCode = AddItemToCartResponseStatusCode.OPERATION_SUCCESS, };
        }

        // Init cart item.
        var newCartItem = InitCartItem(
            addItemToCartRequest: request,
            cardId: cartId,
            userId: Guid.Parse(userId),
            product: foundProduct
        );

        // Add item to cart command.
        var dbResult =
            await _unitOfWork.CartItemFeature.AddItemToCartRepository.CreateCartItemCommandAsync(
                cartItems: newCartItem,
                cancellationToken: cancellationToken
            );

        // Responds if database result command fail.
        if (!dbResult)
        {
            return new() { StatusCode = AddItemToCartResponseStatusCode.DATABASE_OPERATION_FAIL, };
        }

        // Response successfully.
        return new AddItemToCartResponse()
        {
            StatusCode = AddItemToCartResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private CartItem InitCartItem(
        AddItemToCartRequest addItemToCartRequest,
        Data.Shared.Entities.Product product,
        Guid cardId,
        Guid userId
    )
    {
        return new()
        {
            ProductId = addItemToCartRequest.ProductId,
            CartId = cardId,
            Quantity = addItemToCartRequest.Quantity,
            TotalCost = product.QuantityCurrent * product.Price * product.Discount / 100.0m,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
        };
    }
}
