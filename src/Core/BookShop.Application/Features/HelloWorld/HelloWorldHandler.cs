using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Mail;

namespace BookShop.Application.Features.HelloWorld;

/// <summary>
///     Hello World Handler
/// </summary>
public class HelloWorldHandler : IFeatureHandler<HelloWorldRequest, HelloWorldResponse>
{
    private readonly ISendingMailHandler _sendingMailHandler;

    public HelloWorldHandler(ISendingMailHandler sendingMailHandler)
    {
        _sendingMailHandler = sendingMailHandler;
    }

    public async Task<HelloWorldResponse> HandlerAsync(
        HelloWorldRequest request,
        CancellationToken cancellationToken
    )
    {
        var name = request.Name;

        var message = new AppMailContent()
        {
            To = "datnvde180922@fpt.edu.vn",
            Subject = "Hello dat",
            Body = "Hihi"
        };

        var mailContent = await _sendingMailHandler.GetUserAccountConfirmationMailContentAsync(
            "datnvde180922@fpt.edu.vn",
            "Chào đạt lỏ",
            "kakakaka",
            "hohohoho",
            cancellationToken
        );

        var result = await _sendingMailHandler.SendAsync(
            mailContent: mailContent,
            cancellationToken: cancellationToken
        );

        return new HelloWorldResponse()
        {
            ResponseBody = new() { Message = $"Hello {name}" },
            StatusCode = HelloWorldResponseStatusCode.OPERATION_SUCCESS
        };
    }
}
