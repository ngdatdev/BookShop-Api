using BookShop.Application.Shared.Features;

namespace BookShop.Application.Features.Auth.Logout;

/// <summary>
///     Logout Response
/// </summary>
public class LogoutResponse : IFeatureResponse
{
    public LogoutResponseStatusCode StatusCode { get; init; }  
    
}
