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
            public Guid OrderId { get; set; }

            public decimal Amount { get; set; }

            public string Status { get; set; }

            public string Method { get; set; }

            public DateTime PaymentDate { get; set; }

            public string TransactionId { get; set; }
        }
    }
}
