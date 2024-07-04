using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Orders.RestoreOrderById;

/// <summary>
///     RestoreOrderById Handler
/// </summary>
public class RestoreOrderByIdHandler
    : IFeatureHandler<RestoreOrderByIdRequest, RestoreOrderByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public RestoreOrderByIdHandler(IUnitOfWork unitOfWork)
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
    public async Task<RestoreOrderByIdResponse> HandlerAsync(
        RestoreOrderByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is order found by order id.
        var isOrderFound =
            await _unitOfWork.OrderFeature.RestoreOrderByIdRepository.IsOrderFoundByIdQueryAsync(
                orderId: request.OrderId,
                cancellationToken: cancellationToken
            );

        // Responds if order is not found.
        if (!isOrderFound)
        {
            return new() { StatusCode = RestoreOrderByIdResponseStatusCode.ORDER_ID_IS_NOT_FOUND, };
        }

        // Is order removed temporarily.
        var isOrderRemovedTemporarily =
            await _unitOfWork.OrderFeature.RestoreOrderByIdRepository.IsOrderTemporarilyRemovedByIdQueryAsync(
                orderId: request.OrderId,
                cancellationToken: cancellationToken
            );

        // Responds if order is temporarily removed.
        if (!isOrderRemovedTemporarily)
        {
            return new()
            {
                StatusCode = RestoreOrderByIdResponseStatusCode.ORDER_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove order temporarily command.
        var dbResult =
            await _unitOfWork.OrderFeature.RestoreOrderByIdRepository.RestoreOrderByIdCommandAsync(
                orderId: request.OrderId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RestoreOrderByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RestoreOrderByIdResponse()
        {
            StatusCode = RestoreOrderByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
