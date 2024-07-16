using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToCancel;

/// <summary>
///     SwitchOrderStatusToCancel Response
/// </summary>
public class SwitchOrderStatusToCancelResponse : IFeatureResponse
{
    public SwitchOrderStatusToCancelResponseStatusCode StatusCode { get; init; }
}
