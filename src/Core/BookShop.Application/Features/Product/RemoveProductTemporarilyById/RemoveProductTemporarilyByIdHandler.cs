using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Product.RemoveProductTemporarilyById;

/// <summary>
///     RemoveProductTemporarilyById Handler
/// </summary>
public class RemoveProductTemporarilyByIdHandler
    : IFeatureHandler<RemoveProductTemporarilyByIdRequest, RemoveProductTemporarilyByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveProductTemporarilyByIdHandler(
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
    public async Task<RemoveProductTemporarilyByIdResponse> HandlerAsync(
        RemoveProductTemporarilyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check product id is exist in database.
        var isProductIdFound =
            await _unitOfWork.ProductFeature.RemoveProductTemporarilyByIdRepository.IsProductFoundByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if product is not found.
        if (!isProductIdFound)
        {
            return new()
            {
                StatusCode = RemoveProductTemporarilyByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND
            };
        }
        // Check product is temporarily removed by id.
        var isProductTemporarilyRemoved =
            await _unitOfWork.ProductFeature.RemoveProductTemporarilyByIdRepository.IsProductTemporarilyRemovedByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if products is temporarily removed.
        if (isProductTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveProductTemporarilyByIdResponseStatusCode.PRODUCT_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Remove product temporarily command.
        var dbResult =
            await _unitOfWork.ProductFeature.RemoveProductTemporarilyByIdRepository.RemoveProductTemporarilyByIdCommandAsync(
                productId: request.ProductId,
                removedAt: DateTime.UtcNow,
                removedBy: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveProductTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveProductTemporarilyByIdResponse()
        {
            StatusCode = RemoveProductTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
