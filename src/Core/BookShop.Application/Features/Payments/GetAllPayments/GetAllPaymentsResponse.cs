using System;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.Payments.GetAllPayments;

/// <summary>
///     GetAllPayments Response
/// </summary>
public class GetAllPaymentsResponse : IFeatureResponse
{
    public GetAllPaymentsResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<Payment> Payments { get; init; }

        public sealed class Payment
        {
            public Guid OrderId { get; set; }

            public decimal Amount { get; set; }

            public string Status { get; set; }

            public string Method { get; set; }

            public DateTime PaymentDate { get; set; }

            public string TransactionId { get; set; }
        }
    }
}
