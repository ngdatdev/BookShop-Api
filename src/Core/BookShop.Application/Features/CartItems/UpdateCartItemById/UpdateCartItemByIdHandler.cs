using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.CartItems.UpdateCartItemById;

/// <summary>
///     UpdateCartItemById Handler
/// </summary>
public class UpdateCartItemByIdHandler
    : IFeatureHandler<UpdateCartItemByIdRequest, UpdateCartItemByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateCartItemByIdHandler(
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
    public async Task<UpdateCartItemByIdResponse> HandlerAsync(
        UpdateCartItemByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check cart item is found.
        var foundCartItem =
            await _unitOfWork.CartItemFeature.UpdateCartItemByIdRepository.FindCartItemByIdQueryAsync(
                cartItemId: request.CartItemId,
                cancellationToken: cancellationToken
            );

        // Responds if cart id is not found.
        if (Equals(objA: foundCartItem, objB: default))
        {
            return new()
            {
                StatusCode = UpdateCartItemByIdResponseStatusCode.CART_ITEM_IS_NOT_FOUND
            };
        }

        // Responds if quantity of request higher than current quantity.
        if (foundCartItem.Product.QuantityCurrent < request.Quantity)
        {
            return new()
            {
                StatusCode = UpdateCartItemByIdResponseStatusCode.QUANTITY_IS_NOT_ENOUGH,
            };
        }

        // Get userId from claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Update quantity cart item
        var updateQuantityCartItem =
            await _unitOfWork.CartItemFeature.UpdateCartItemByIdRepository.UpdateCartItemCommandAsync(
                cartItemId: request.CartItemId,
                quantity: request.Quantity,
                updateAt: DateTime.UtcNow,
                updateBy: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if database is fail.
        if (!updateQuantityCartItem)
        {
            return new()
            {
                StatusCode = UpdateCartItemByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdateCartItemByIdResponse()
        {
            StatusCode = UpdateCartItemByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
