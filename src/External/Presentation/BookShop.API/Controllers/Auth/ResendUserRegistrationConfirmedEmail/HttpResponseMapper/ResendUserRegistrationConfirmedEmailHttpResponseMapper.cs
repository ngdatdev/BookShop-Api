namespace BookShop.API.Controllers.Auth.ResendUserRegistrationConfirmedEmail.HttpResponseMapper;

/// <summary>
///     ResendUserRegistrationConfirmedEmail extension method
/// </summary>
internal static class ResendUserRegistrationConfirmedEmailHttpResponseMapper
{
    private static ResendUserRegistrationConfirmedEmailHttpResponseManager _ResendUserRegistrationConfirmedEmailHttpResponseManager;

    internal static ResendUserRegistrationConfirmedEmailHttpResponseManager Get()
    {
        return _ResendUserRegistrationConfirmedEmailHttpResponseManager ??= new();
    }
}
