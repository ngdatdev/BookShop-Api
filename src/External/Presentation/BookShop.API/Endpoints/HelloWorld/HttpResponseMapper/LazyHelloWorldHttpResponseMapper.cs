using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.API.Endpoints.HelloWorld.HttpResponseMapper;

/// <summary>
///     Hello world extension method
/// </summary>
internal static class LazyHelloWorldHttpResponseMapper
{
    /// <summary>
    ///     Hello world http response manager
    /// </summary>
    private static HelloWorldHttpResponseManager _helloWorldHttpResponseManager;

    internal static HelloWorldHttpResponseManager Get()
    {
        return _helloWorldHttpResponseManager ??= new();
    }
}
