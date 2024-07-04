using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Orders.RemoveOrderPermanentlyById;

/// <summary>
///     RemoveOrderPermanentlyById Handler
/// </summary>
public class RemoveOrderPermanentlyByIdHandler
    : IFeatureHandler<RemoveOrderPermanentlyByIdRequest, RemoveOrderPermanentlyByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveOrderPermanentlyByIdHandler(
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
    public async Task<RemoveOrderPermanentlyByIdResponse> HandlerAsync(
        RemoveOrderPermanentlyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check order id is exist in database.
        var isOrderIdFound =
            await _unitOfWork.OrderFeature.RemoveOrderPermanentlyByIdRepository.IsOrderFoundByIdQueryAsync(
                orderId: request.OrderId,
                cancellationToken: cancellationToken
            );

        // Responds if order is not found.
        if (!isOrderIdFound)
        {
            return new()
            {
                StatusCode = RemoveOrderPermanentlyByIdResponseStatusCode.ORDER_ID_IS_NOT_FOUND
            };
        }
        // Check order is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.OrderFeature.RemoveOrderPermanentlyByIdRepository.IsOrderTemporarilyRemovedByIdQueryAsync(
                orderId: request.OrderId,
                cancellationToken: cancellationToken
            );

        // Responds if orders is temporarily removed.
        if (!isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveOrderPermanentlyByIdResponseStatusCode.ORDER_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove order temporarily command.
        var dbResult =
            await _unitOfWork.OrderFeature.RemoveOrderPermanentlyByIdRepository.DeleteOrderPermanentlyByIdCommandAsync(
                orderId: request.OrderId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveOrderPermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveOrderPermanentlyByIdResponse()
        {
            StatusCode = RemoveOrderPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
