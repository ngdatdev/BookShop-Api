using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Payments.GetAllPayments;

/// <summary>
///     GetAllPayments Handler
/// </summary>
public class GetAllPaymentsHandler : IFeatureHandler<GetAllPaymentsRequest, GetAllPaymentsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPaymentsHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllPaymentsResponse> HandlerAsync(
        GetAllPaymentsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Find payments is not removed temporaliry.
        var payments =
            await _unitOfWork.PaymentFeature.GetAllPaymentsRepository.FindAllPaymentsQueryAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Get total number payments record.
        var totalPayments =
            await _unitOfWork.PaymentFeature.GetAllPaymentsRepository.GetTotalNumberOfPayments(
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllPaymentsResponse()
        {
            StatusCode = GetAllPaymentsResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Payments = new()
                {
                    Contents = payments.Select(payment => new GetAllPaymentsResponse.Body.Payment()
                    {
                        Amount = payment.Amount,
                        Method = payment.Method.ToString(),
                        OrderId = payment.OrderId,
                        PaymentDate = payment.PaymentDate,
                        Status = payment.Status.ToString(),
                        TransactionId = payment.TransactionId
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)totalPayments / request.PageSize)
                }
            }
        };
    }
}
