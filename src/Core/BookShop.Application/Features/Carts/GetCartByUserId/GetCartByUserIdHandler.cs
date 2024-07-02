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

namespace BookShop.Application.Features.Carts.GetCartByUserId;

/// <summary>
///     GetCartByUserId Handler
/// </summary>
public class GetCartByUserIdHandler
    : IFeatureHandler<GetCartByUserIdRequest, GetCartByUserIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetCartByUserIdHandler(
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
    public async Task<GetCartByUserIdResponse> HandlerAsync(
        GetCartByUserIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from claims json web token.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Find cart by userId.
        var cart = await _unitOfWork.CartFeature.GetCartByIdRepository.FindCartByIdQueryAsync(
            userId: Guid.Parse(input: userId),
            cancellationToken: cancellationToken
        );

        // Responds if cart is not found.
        if (Equals(objA: cart, objB: default))
        {
            return new() { StatusCode = GetCartByUserIdResponseStatusCode.CART_IS_NOT_FOUND, };
        }

        // Response successfully.
        return new GetCartByUserIdResponse()
        {
            StatusCode = GetCartByUserIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(
                    cartItem => new GetCartByUserIdResponse.Body.CartItem()
                    {
                        CartItemId = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        FullName = cartItem.Product.FullName,
                        Author = cartItem.Product.Author,
                        ImageUrl = cartItem.Product.ImageUrl,
                        DiscountPrice = cartItem.Product.Price.ToString("0.000"),
                        RootPrice = (
                            cartItem.Product.Price * (1 - (cartItem.Product.Discount / 100.0m))
                        ).ToString("0.000"),
                        Size = cartItem.Product.Size,
                        Quantity = cartItem.Quantity,
                    }
                ),
                NumberOfItems = cart.CartItems.Count(),
                FinalPrice = cart
                    .CartItems.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity)
                    .ToString("0.000"),
                TotalPrice = cart
                    .CartItems.Sum(cartItem =>
                        cartItem.Product.Price
                        * (1 - (cartItem.Product.Discount / 100.0m))
                        * cartItem.Quantity
                    )
                    .ToString("0.000"),
            }
        };
    }
}
