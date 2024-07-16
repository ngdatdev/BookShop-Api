using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.RestoreOrderStatusToConfirm;

/// <summary>
///     RestoreOrderStatusToConfirm Response
/// </summary>
public class RestoreOrderStatusToConfirmResponse : IFeatureResponse
{
    public RestoreOrderStatusToConfirmResponseStatusCode StatusCode { get; init; }
}
