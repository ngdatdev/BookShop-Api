namespace BookShop.Application.Features.HelloWorld;

/// <summary>
///     Extension Method for hello world features.
/// </summary>
public static class ExtensionMethod
{
    public static string ToAppCode(this HelloWorldResponseStatusCode statusCode)
    {
        var messageStatusCode = statusCode.ToString().Replace("_", " ");
        return $"{nameof(HelloWorld)}: {messageStatusCode}";
    }
}
