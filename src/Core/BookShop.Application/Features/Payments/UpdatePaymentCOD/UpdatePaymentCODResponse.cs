using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Payments.UpdatePaymentCOD;

/// <summary>
///     UpdatePaymentCOD Response
/// </summary>
public class UpdatePaymentCODResponse : IFeatureResponse
{
    public UpdatePaymentCODResponseStatusCode StatusCode { get; init; }
}
