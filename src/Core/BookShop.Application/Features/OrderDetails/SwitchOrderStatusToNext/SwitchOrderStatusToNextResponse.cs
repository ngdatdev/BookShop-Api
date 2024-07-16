using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.SwitchOrderStatusToNext;

/// <summary>
///     SwitchOrderStatusToNext Response
/// </summary>
public class SwitchOrderStatusToNextResponse : IFeatureResponse
{
    public SwitchOrderStatusToNextResponseStatusCode StatusCode { get; init; }
}
