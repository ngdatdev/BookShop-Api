using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.HelloWorld;

/// <summary>
///     Hello World Handler
/// </summary>
public class HelloWorldHandler : IFeatureHandler<HelloWorldRequest, HelloWorldResponse>
{
    public async Task<HelloWorldResponse> HandlerAsync(
        HelloWorldRequest request,
        CancellationToken cancellationToken
    )
    {
        var name = request.Name;
        return new HelloWorldResponse()
        {
            ResponseBody = new() { Message = $"Hello {name}" },
            StatusCode = HelloWorldResponseStatusCode.OPERATION_SUCCESS
        };
    }
}
