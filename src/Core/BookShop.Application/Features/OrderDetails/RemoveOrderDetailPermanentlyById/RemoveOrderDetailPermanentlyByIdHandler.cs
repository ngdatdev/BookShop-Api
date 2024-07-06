using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.OrderDetails.RemoveOrderDetailPermanentlyById;

/// <summary>
///     RemoveOrderDetailPermanentlyById Handler
/// </summary>
public class RemoveOrderDetailPermanentlyByIdHandler
    : IFeatureHandler<
        RemoveOrderDetailPermanentlyByIdRequest,
        RemoveOrderDetailPermanentlyByIdResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderDetailPermanentlyByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
    public async Task<RemoveOrderDetailPermanentlyByIdResponse> HandlerAsync(
        RemoveOrderDetailPermanentlyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check order id is exist in database.
        var isOrderIdFound =
            await _unitOfWork.OrderDetailFeature.RemoveOrderDetailPermanentlyByIdRepository.IsOrderDetailFoundByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if order is not found.
        if (!isOrderIdFound)
        {
            return new()
            {
                StatusCode =
                    RemoveOrderDetailPermanentlyByIdResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND
            };
        }
        // Check order is temporarily removed by id.
        var isOrderTemporarilyRemoved =
            await _unitOfWork.OrderDetailFeature.RemoveOrderDetailPermanentlyByIdRepository.IsOrderDetailTemporarilyRemovedByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if orders is temporarily removed.
        if (!isOrderTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveOrderDetailPermanentlyByIdResponseStatusCode.ORDER_DETAIL_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove order temporarily command.
        var dbResult =
            await _unitOfWork.OrderDetailFeature.RemoveOrderDetailPermanentlyByIdRepository.DeleteOrderDetailPermanentlyByIdCommandAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode =
                    RemoveOrderDetailPermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveOrderDetailPermanentlyByIdResponse()
        {
            StatusCode = RemoveOrderDetailPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
