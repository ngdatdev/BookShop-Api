using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Product.RemoveProductPermanentlyById;

/// <summary>
///     RemoveProductPermanentlyById Handler
/// </summary>
public class RemoveProductPermanentlyByIdHandler
    : IFeatureHandler<RemoveProductPermanentlyByIdRequest, RemoveProductPermanentlyByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveProductPermanentlyByIdHandler(
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
    public async Task<RemoveProductPermanentlyByIdResponse> HandlerAsync(
        RemoveProductPermanentlyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check product id is exist in database.
        var isProductIdFound =
            await _unitOfWork.ProductFeature.RemoveProductPermanentlyByIdRepository.IsProductFoundByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if product is not found.
        if (!isProductIdFound)
        {
            return new()
            {
                StatusCode = RemoveProductPermanentlyByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND
            };
        }
        // Check product is temporarily removed by id.
        var isProductTemporarilyRemoved =
            await _unitOfWork.ProductFeature.RemoveProductPermanentlyByIdRepository.IsProductTemporarilyRemovedByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if products is temporarily removed.
        if (!isProductTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveProductPermanentlyByIdResponseStatusCode.PRODUCT_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Remove product temporarily command.
        var dbResult =
            await _unitOfWork.ProductFeature.RemoveProductPermanentlyByIdRepository.RemoveProductPermanentlyByIdCommandAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveProductPermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveProductPermanentlyByIdResponse()
        {
            StatusCode = RemoveProductPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
