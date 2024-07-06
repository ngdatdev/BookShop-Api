using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.OrderDetails.RestoreOrderDetailById;

/// <summary>
///     RestoreOrderDetailById Response
/// </summary>
public class RestoreOrderDetailByIdResponse : IFeatureResponse
{
    public RestoreOrderDetailByIdResponseStatusCode StatusCode { get; init; }
}
