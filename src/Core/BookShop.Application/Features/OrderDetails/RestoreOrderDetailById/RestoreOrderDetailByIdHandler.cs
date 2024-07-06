using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.OrderDetails.RestoreOrderDetailById;

/// <summary>
///     RestoreOrderDetailById Handler
/// </summary>
public class RestoreOrderDetailByIdHandler
    : IFeatureHandler<RestoreOrderDetailByIdRequest, RestoreOrderDetailByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public RestoreOrderDetailByIdHandler(IUnitOfWork unitOfWork)
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
    public async Task<RestoreOrderDetailByIdResponse> HandlerAsync(
        RestoreOrderDetailByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is order found by order id.
        var isOrderFound =
            await _unitOfWork.OrderDetailFeature.RestoreOrderDetailByIdRepository.IsOrderDetailFoundByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if order is not found.
        if (!isOrderFound)
        {
            return new()
            {
                StatusCode = RestoreOrderDetailByIdResponseStatusCode.ORDER_DETAIL_ID_IS_NOT_FOUND,
            };
        }

        // Is order removed temporarily.
        var isOrderRemovedTemporarily =
            await _unitOfWork.OrderDetailFeature.RestoreOrderDetailByIdRepository.IsOrderDetailTemporarilyRemovedByIdQueryAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if order is temporarily removed.
        if (!isOrderRemovedTemporarily)
        {
            return new()
            {
                StatusCode =
                    RestoreOrderDetailByIdResponseStatusCode.ORDER_DETAIL_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove order temporarily command.
        var dbResult =
            await _unitOfWork.OrderDetailFeature.RestoreOrderDetailByIdRepository.RestoreOrderDetailByIdCommandAsync(
                orderDetailId: request.OrderDetailId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RestoreOrderDetailByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RestoreOrderDetailByIdResponse()
        {
            StatusCode = RestoreOrderDetailByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
