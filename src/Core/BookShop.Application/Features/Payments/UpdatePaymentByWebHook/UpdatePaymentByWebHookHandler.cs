using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.PaymentGateway;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;

namespace BookShop.Application.Features.Payments.UpdatePaymentByWebHook;

/// <summary>
///     UpdatePaymentByWebHook Handler
/// </summary>
public class UpdatePaymentByWebHookHandler
    : IFeatureHandler<UpdatePaymentByWebHookRequest, UpdatePaymentByWebHookResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentHandler _paymentHandler;

    public UpdatePaymentByWebHookHandler(IUnitOfWork unitOfWork, IPaymentHandler paymentHandler)
    {
        _unitOfWork = unitOfWork;
        _paymentHandler = paymentHandler;
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
    public async Task<UpdatePaymentByWebHookResponse> HandlerAsync(
        UpdatePaymentByWebHookRequest request,
        CancellationToken cancellationToken
    )
    {
        // Validate signature.
        var isValidSignature = _paymentHandler.VerifyWebhookData(
            webhookType: new WebhookType
            {
                Code = request.Code,
                Desc = request.Desc,
                Success = request.Success,
                Signature = request.Signature,
                Data = new WebhookType.WebhookData
                {
                    AccountNumber = request.Data.AccountNumber,
                    Amount = request.Data.Amount,
                    Description = request.Data.Description,
                    Reference = request.Data.Reference,
                    TransactionDateTime = request.Data.TransactionDateTime,
                    VirtualAccountNumber = request.Data.VirtualAccountNumber,
                    CounterAccountBankId = request.Data.CounterAccountBankId,
                    CounterAccountBankName = request.Data.CounterAccountBankName,
                    CounterAccountName = request.Data.CounterAccountName,
                    CounterAccountNumber = request.Data.CounterAccountNumber,
                    VirtualAccountName = request.Data.VirtualAccountName,
                    OrderCode = request.Data.OrderCode,
                    Currency = request.Data.Currency,
                    PaymentLinkId = request.Data.PaymentLinkId,
                    Code = request.Data.Code,
                    Desc = request.Data.Desc
                }
            }
        );

        // Respond if it is not valid.
        if (!isValidSignature)
        {
            return new()
            {
                StatusCode = UpdatePaymentByWebHookResponseStatusCode.WEBHOOK_RETURN_ERROR,
            };
        }

        // Respond if code failure.
        if (!request.Success)
        {
            return new()
            {
                StatusCode = UpdatePaymentByWebHookResponseStatusCode.WEBHOOK_RETURN_ERROR
            };
        }

        // Get Payment by orderId.
        var payment =
            await _unitOfWork.PaymentFeature.UpdatePaymentByWebHookRepository.FindPaymentByOrderIdQueryAsync(
                orderId: new Guid(request.Data.OrderCode),
                cancellationToken: cancellationToken
            );

        // Respond if payment is not found.
        if (Equals(objA: payment, objB: default))
        {
            return new()
            {
                StatusCode = UpdatePaymentByWebHookResponseStatusCode.ORDER_IS_NOT_FOUND,
            };
        }

        // Create payment instance.
        var paymentUpdate = InitPaymentInstance(data: request.Data);

        // Update payment information to database.
        var dbResult =
            await _unitOfWork.PaymentFeature.UpdatePaymentByWebHookRepository.UpdatePaymentByWebHookCommandAsync(
                updatePayment: paymentUpdate,
                cancellationToken: cancellationToken
            );

        // Repond if datbase operation failure.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = UpdatePaymentByWebHookResponseStatusCode.DATABASE_OPERATION_FAIL,
            };
        }

        // Response successfully.
        return new UpdatePaymentByWebHookResponse()
        {
            StatusCode = UpdatePaymentByWebHookResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private Payment InitPaymentInstance(UpdatePaymentByWebHookRequest.WebhookData data)
    {
        return new()
        {
            OrderId = new Guid(data.OrderCode),
            Amount = data.Amount,
            Status = PaymentStatus.Completed,
            PaymentDate = DateTime.UtcNow,
            TransactionId = data.Reference
        };
    }
}
