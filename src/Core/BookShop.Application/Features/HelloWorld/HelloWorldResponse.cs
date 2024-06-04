using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.HelloWorld;

/// <summary>
///     Hello World Response
/// </summary>
public class HelloWorldResponse : IFeatureResponse
{
    public HelloWorldResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string Message { get; init; }
    }
}
