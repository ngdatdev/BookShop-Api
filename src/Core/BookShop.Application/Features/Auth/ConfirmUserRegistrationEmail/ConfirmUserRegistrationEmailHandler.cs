using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Mail;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace BookShop.Application.Features.Auth.ConfirmUserRegistrationEmail;

/// <summary>
///     ConfirmUserRegistrationEmail Handler
/// </summary>
public class ConfirmUserRegistrationEmailHandler
    : IFeatureHandler<ConfirmUserRegistrationEmailRequest, ConfirmUserRegistrationEmailResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly ISendingMailHandler _sendingMailHandler;

    public ConfirmUserRegistrationEmailHandler(
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
    public async Task<ConfirmUserRegistrationEmailResponse> HandlerAsync(
        ConfirmUserRegistrationEmailRequest request,
        CancellationToken cancellationToken
    )
    {
        // Is user not temporarily removed.
        var isUserNotTemporarilyRemoved =
            await _unitOfWork.AuthFeature.ConfirmUserRegistrationEmailRepository.IsUserNotTemporarilyRemovedQueryAsync(
                userId: foundUser.Id,
                cancellationToken: cancellationToken
            );

        // Responds if user is temporarily removed.
        if (!isUserNotTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    ConfirmUserRegistrationEmailResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
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
                StatusCode =
                    ConfirmUserRegistrationEmailResponseStatusCode.NEW_PASSWORD_IS_NOT_VALID
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
                StatusCode = ConfirmUserRegistrationEmailResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Remove the rset password token.
        var dbResult =
            await _unitOfWork.AuthFeature.ConfirmUserRegistrationEmailRepository.RemoveUserResetPasswordTokenCommandAsync(
                resetPasswordToken: request.ResetPasswordToken,
                cancellationToken: cancellationToken
            );

        // Cannot remove the reset password token.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = ConfirmUserRegistrationEmailResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new ConfirmUserRegistrationEmailResponse()
        {
            StatusCode = ConfirmUserRegistrationEmailResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
