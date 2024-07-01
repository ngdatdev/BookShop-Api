using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.CartItems.RemoveCartItemById;

/// <summary>
///     RemoveCartItemById Handler
/// </summary>
public class RemoveCartItemByIdHandler
    : IFeatureHandler<RemoveCartItemByIdRequest, RemoveCartItemByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveCartItemByIdHandler(
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
    public async Task<RemoveCartItemByIdResponse> HandlerAsync(
        RemoveCartItemByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is cart item is found by id.
        var isCartItemFound =
            await _unitOfWork.CartItemFeature.RemoveCartItemByIdRepository.IsCartItemFoundByIdQueryAsync(
                cartItemId: request.CartItemId,
                cancellationToken: cancellationToken
            );

        // Responds if cart item is not found.
        if (!isCartItemFound)
        {
            return new()
            {
                StatusCode = RemoveCartItemByIdResponseStatusCode.CART_ITEM_IS_NOT_FOUND,
            };
        }

        // Remove cart item by id.
        var dbResult =
            await _unitOfWork.CartItemFeature.RemoveCartItemByIdRepository.DeleteCartItemByIdCommandAsync(
                cartItemId: request.CartItemId,
                cancellationToken: cancellationToken
            );

        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveCartItemByIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new RemoveCartItemByIdResponse()
        {
            StatusCode = RemoveCartItemByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
