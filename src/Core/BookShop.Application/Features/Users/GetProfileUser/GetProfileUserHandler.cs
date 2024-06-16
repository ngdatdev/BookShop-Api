using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Features.Auth.Login;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Users.GetProfileUser;

/// <summary>
///     GetProfileUser Handler
/// </summary>
public class GetProfileUserHandler : IFeatureHandler<GetProfileUserRequest, GetProfileUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetProfileUserHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
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
    public async Task<GetProfileUserResponse> HandlerAsync(
        GetProfileUserRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.UserFeature.GetProfileUserRepository.GetUserDetailByUserIdQueryAsync(
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new GetProfileUserResponse()
            {
                StatusCode = GetProfileUserResponseStatusCode.USER_IS_NOT_FOUND
            };
        }

        // Is user not temporarily removed.
        var isUserNotTemporarilyRemoved =
            await _unitOfWork.AuthFeature.LoginRepository.IsUserTemporarilyRemovedQueryAsync(
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Responds if current user is temporarily removed.
        if (!isUserNotTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = GetProfileUserResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Response successfully.
        return new GetProfileUserResponse()
        {
            StatusCode = GetProfileUserResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                User = new()
                {
                    Email = foundUser.User.Email,
                    Username = foundUser.User.UserName,
                    NumberPhone = foundUser.User.PhoneNumber,
                    AvatarUrl = foundUser.AvatarUrl,
                    FirstName = foundUser.FirstName,
                    LastName = foundUser.LastName,
                    Gender = foundUser.Gender,
                    Address = new()
                    {
                        Ward = foundUser.Address.Ward,
                        District = foundUser.Address.District,
                        Province = foundUser.Address.Province,
                    }
                }
            }
        };
    }
}
