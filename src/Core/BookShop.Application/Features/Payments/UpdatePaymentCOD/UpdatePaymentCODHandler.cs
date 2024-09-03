using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Payments.UpdatePaymentCOD;

/// <summary>
///     UpdatePaymentCOD Handler
/// </summary>
public class UpdatePaymentCODHandler
    : IFeatureHandler<UpdatePaymentCODRequest, UpdatePaymentCODResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentCODHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<UpdatePaymentCODResponse> HandlerAsync(
        UpdatePaymentCODRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find payment by orderId.
        var payment =
            await _unitOfWork.PaymentFeature.UpdatePaymentCODRepository.FindPaymentByIdQueryAsync(
                orderId: request.OrderId,
                cancellationToken: cancellationToken
            );

        // Respond if payment is not found.
        if (Equals(objA: payment, objB: default))
        {
            return new()
            {
                StatusCode = UpdatePaymentCODResponseStatusCode.PAYMENT_ID_IS_NOT_FOUND,
            };
        }

        // Is paymet temporarily removed.
        var isPaymentTemporarilyRemoved =
            await _unitOfWork.PaymentFeature.UpdatePaymentCODRepository.IsPaymentTemporarilyRemovedQueryAsync(
                paymentId: payment.Id,
                cancellationToken: cancellationToken
            );

        // Respond if payment is temporarily remoeved.
        if (isPaymentTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = UpdatePaymentCODResponseStatusCode.PAYMENT_TEMPORARILY_REMOVED,
            };
        }

        // Update payment information.
        var dbResult =
            await _unitOfWork.PaymentFeature.UpdatePaymentCODRepository.UpdatePaymentCODCommandAsync(
                updatePayment: payment,
                cancellationToken: cancellationToken
            );

        if (!dbResult)
        {
            return new()
            {
                StatusCode = UpdatePaymentCODResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdatePaymentCODResponse()
        {
            StatusCode = UpdatePaymentCODResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
