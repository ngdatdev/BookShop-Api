using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Orders.RestoreOrderById;

/// <summary>
///     RestoreOrderById Response
/// </summary>
public class RestoreOrderByIdResponse : IFeatureResponse
{
    public RestoreOrderByIdResponseStatusCode StatusCode { get; init; }
}
