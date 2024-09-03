using System;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;

namespace BookShop.Application.Features.Payments.GetPaymentsByMethod;

/// <summary>
///     GetPaymentsByMethod Response
/// </summary>
public class GetPaymentsByMethodResponse : IFeatureResponse
{
    public GetPaymentsByMethodResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public PaginationResponse<Payment> Payments { get; init; }

        public sealed class Payment
        {
            public Guid OrderId { get; init; }

            public decimal Amount { get; init; }

            public string Status { get; init; }

            public string Method { get; init; }

            public DateTime PaymentDate { get; init; }

            public string TransactionId { get; init; }
        }
    }
}
