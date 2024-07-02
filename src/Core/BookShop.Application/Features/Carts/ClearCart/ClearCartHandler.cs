using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Carts.ClearCart;

/// <summary>
///     ClearCart Handler
/// </summary>
public class ClearCartHandler : IFeatureHandler<ClearCartRequest, ClearCartResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClearCartHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
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
    public async Task<ClearCartResponse> HandlerAsync(
        ClearCartRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claims json web token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Find cartId by userId.
        var cartId = await _unitOfWork.CartFeature.ClearCartRepository.FindCartIdByUserIdQueryAsync(
            userId: Guid.Parse(input: userId),
            cancellationToken: cancellationToken
        );

        // Responds if cart is not found.
        if (Equals(objA: cartId, objB: default))
        {
            return new() { StatusCode = ClearCartResponseStatusCode.CART_IS_NOT_FOUND, };
        }

        // Clear cart command.
        var dbResult = await _unitOfWork.CartFeature.ClearCartRepository.ClearCartCommandAsync(
            cartId: cartId,
            cancellationToken: cancellationToken
        );

        if (!dbResult)
        {
            return new() { StatusCode = ClearCartResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new ClearCartResponse()
        {
            StatusCode = ClearCartResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
