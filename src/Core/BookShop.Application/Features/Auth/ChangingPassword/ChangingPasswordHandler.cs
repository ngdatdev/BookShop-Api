using System;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Mail;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Application.Features.Auth.ChangingPassword;

/// <summary>
///     ChangingPassword Handler
/// </summary>
public class ChangingPasswordHandler
    : IFeatureHandler<ChangingPasswordRequest, ChangingPasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly ISendingMailHandler _sendingMailHandler;

    public ChangingPasswordHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        ISendingMailHandler sendingMailHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _sendingMailHandler = sendingMailHandler;
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
    public async Task<ChangingPasswordResponse> HandlerAsync(
        ChangingPasswordRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is reset password token found by its value.
        var isResetPasswordTokenFound =
            await _unitOfWork.ChangingPasswordRepository.IsResetPasswordTokenFoundByItsValueQueryAsync(
                passwordResetToken: request.ResetPasswordToken,
                cancellationToken: cancellationToken
            );

        // Responds if reset password token is not found by its value.
        if (!isResetPasswordTokenFound)
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.RESET_PASSWORD_TOKEN_IS_NOT_FOUND
            };
        }

        // Get the user token by reset password token.
        var foundUserToken =
            await _unitOfWork.ChangingPasswordRepository.FindUserTokenByResetPasswordTokenQueryAsync(
                passwordResetToken: request.ResetPasswordToken,
                cancellationToken: cancellationToken
            );

        // Find the user by user id.
        var foundUser = await _userManager.FindByIdAsync(userId: foundUserToken.UserId.ToString());

        // Is user not temporarily removed.
        var isUserNotTemporarilyRemoved =
            await _unitOfWork.ChangingPasswordRepository.IsUserNotTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: cancellationToken
            );

        // Responds if user is temporarily removed.
        if (!isUserNotTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Is new user password valid.
        var isPasswordValid = await ValidateUserPasswordAsync(
            newUser: foundUser,
            newPassword: request.NewPassword
        );

        // Responds if is not valid.
        if (!isPasswordValid)
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.NEW_PASSWORD_IS_NOT_VALID
            };
        }

        // Reset user password with new password by reset password token.
        var resetPasswordResult = await _userManager.ResetPasswordAsync(
            user: foundUser,
            token: request.ResetPasswordToken,
            newPassword: request.NewPassword
        );

        // Responds if cannot reset user password.
        if (!resetPasswordResult.Succeeded)
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Remove the rset password token.
        var dbResult =
            await _unitOfWork.ChangingPasswordRepository.RemoveUserResetPasswordTokenCommandAsync(
                resetPasswordToken: request.ResetPasswordToken,
                cancellationToken: cancellationToken
            );

        // Cannot remove the reset password token.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = ChangingPasswordResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new ChangingPasswordResponse()
        {
            StatusCode = ChangingPasswordResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    /// <summary>
    ///     Validates the password of a newly created user.
    /// </summary>
    /// <param name="newUser">
    ///     The newly created user.
    /// </param>
    /// <param name="newPassword">
    ///     The password to be validated.
    /// </param>
    /// <returns>
    ///     True if the password is valid, False otherwise.
    /// </returns>
    private async Task<bool> ValidateUserPasswordAsync(User newUser, string newPassword)
    {
        IdentityResult result = default;

        foreach (var validator in _userManager.PasswordValidators)
        {
            result = await validator.ValidateAsync(
                manager: _userManager,
                user: newUser,
                password: newPassword
            );
        }

        if (Equals(objA: result, objB: default))
        {
            return false;
        }

        return result.Succeeded;
    }
}
