using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Product.RestoreProductById;

/// <summary>
///     RestoreProductById Handler
/// </summary>
public class RestoreProductByIdHandler
    : IFeatureHandler<RestoreProductByIdRequest, RestoreProductByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RestoreProductByIdHandler(
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
    public async Task<RestoreProductByIdResponse> HandlerAsync(
        RestoreProductByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check product id is exist in database.
        var isProductIdFound =
            await _unitOfWork.ProductFeature.RestoreProductByIdRepository.IsProductFoundByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if product is not found.
        if (!isProductIdFound)
        {
            return new()
            {
                StatusCode = RestoreProductByIdResponseStatusCode.PRODUCT_ID_IS_NOT_FOUND
            };
        }
        // Check product is temporarily removed by id.
        var isProductTemporarilyRemoved =
            await _unitOfWork.ProductFeature.RestoreProductByIdRepository.IsProductTemporarilyRemovedByIdQueryAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if products is temporarily removed.
        if (!isProductTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RestoreProductByIdResponseStatusCode.PRODUCT_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Remove product temporarily command.
        var dbResult =
            await _unitOfWork.ProductFeature.RestoreProductByIdRepository.RestoreProductByIdCommandAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RestoreProductByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RestoreProductByIdResponse()
        {
            StatusCode = RestoreProductByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
