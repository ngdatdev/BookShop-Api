using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Authentication.Jwt;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BookShop.Application.Features.Auth.LoginByGoogle;

/// <summary>
///     LoginByGoogle Handler
/// </summary>
public class LoginByGoogleHandler : IFeatureHandler<LoginByGoogleRequest, LoginByGoogleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRefreshTokenHandler _refreshTokenHandler;
    private readonly IAccessTokenHandler _accessTokenHandler;

    public LoginByGoogleHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IRefreshTokenHandler refreshTokenHandler,
        IAccessTokenHandler accessTokenHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        _refreshTokenHandler = refreshTokenHandler;
        _accessTokenHandler = accessTokenHandler;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="request">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    public async Task<LoginByGoogleResponse> HandlerAsync(
        LoginByGoogleRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get sub from google authentication.
        var googleUser = await ValidateGoogleToken(idToken: request.IdToken);

        if (Equals(objA: googleUser, objB: default))
        {
            return new() { StatusCode = LoginByGoogleResponseStatusCode.UNAUTHORIZE };
        }

        // Init list of user claims.
        List<Claim> userClaims =
        [
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Sub, value: googleUser.Sub),
            new(type: "role", value: "user")
        ];

        // Generate new access token.
        var newAccessToken = _accessTokenHandler.GenerateSigningToken(claims: userClaims);

        // Response successfully.
        return new LoginByGoogleResponse()
        {
            StatusCode = LoginByGoogleResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                AccessToken = newAccessToken,
                User = new()
                {
                    Email = googleUser.Email,
                    AvatarUrl = googleUser.Picture,
                    FirstName = googleUser.Name,
                    LastName = default,
                }
            },
        };
    }

    private async Task<GoogleUser> ValidateGoogleToken(string idToken)
    {
        var httpClient = new HttpClient();
        string response;
        try
        {
            response = await httpClient.GetStringAsync(
                $"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}"
            );
        }

        catch
        {
            return null;
        }

        var googleUser = JsonConvert.DeserializeObject<GoogleUser>(response);

        return googleUser;
    }

    public sealed class GoogleUser
    {
        public string Sub { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
    }
}
