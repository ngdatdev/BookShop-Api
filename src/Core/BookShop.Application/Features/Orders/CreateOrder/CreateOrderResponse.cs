using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Orders.CreateOrder;

/// <summary>
///     CreateOrder Response
/// </summary>
public class CreateOrderResponse : IFeatureResponse
{
    public CreateOrderResponseStatusCode StatusCode { get; init; }
}
