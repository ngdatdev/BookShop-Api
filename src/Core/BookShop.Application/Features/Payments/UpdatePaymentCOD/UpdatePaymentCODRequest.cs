using System;
using System.Collections.Generic;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Payments.UpdatePaymentCOD;

/// <summary>
///     UpdatePaymentCOD Request
/// </summary>
public class UpdatePaymentCODRequest : IFeatureRequest<UpdatePaymentCODResponse>
{
    public Guid OrderId { get; init; }

    public string Status { get; init; }

    public DateTime PaymentDate { get; init; }
}
