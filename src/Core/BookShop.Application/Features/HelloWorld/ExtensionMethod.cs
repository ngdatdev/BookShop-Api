namespace BookShop.Application.Features.HelloWorld;

/// <summary>
///     Extension Method for forgot password features.
/// </summary>
public static class ExtensionMethod
{
    /// <summary>
    ///     Mapping from feature response status code to
    ///     app code.
    /// </summary>
    /// <param name="statusCode">
    ///     Feature response status code
    /// </param>
    /// <returns>
    ///     New app code.
    /// </returns>
    public static string ToAppCode(this HelloWorldResponseStatusCode statusCode)
    {
        var messageStatusCode = statusCode.ToString().Replace("_", " ");
        return $"{nameof(HelloWorld)}: {messageStatusCode}";
    }
}
