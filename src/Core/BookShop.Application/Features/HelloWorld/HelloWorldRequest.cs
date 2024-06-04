using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.HelloWorld;

/// <summary>
///     Hello World Request
/// </summary>
public class HelloWorldRequest : IFeatureRequest<HelloWorldResponse>
{
    public string Name { get; set; }
}
