using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.CartItems.AddItemToCart;

/// <summary>
///     AddItemToCart Handler
/// </summary>
public class AddItemToCartHandler : IFeatureHandler<AddItemToCartRequest, AddItemToCartResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddItemToCartHandler(
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

        // Responds if cart is not found.

        // Check productId is exist.

        // Responds if product is not found.

        // Add item to cart command.

        // Responds if database result command fail.

        // Response successfully.
        return new AddItemToCartResponse()
        {
            StatusCode = AddItemToCartResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
