using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorage;
using BookShop.Application.Shared.Mail;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace BookShop.Application.Features.Auth.RegisterAsUser;

/// <summary>
///     RegisterAsUser Handler
/// </summary>
public class RegisterAsUserHandler : IFeatureHandler<RegisterAsUserRequest, RegisterAsUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;
    private readonly ISendingMailHandler _sendingMailHandler;

    public RegisterAsUserHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler,
        ISendingMailHandler sendingMailHandler
    )
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _defaultUserAvatarAsUrlHandler = defaultUserAvatarAsUrlHandler;
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
    public async Task<RegisterAsUserResponse> HandlerAsync(
        RegisterAsUserRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check email is registered by other users.
        var isEmailExist =
            await _unitOfWork.AuthFeature.RegisterAsUserRepository.IsUserFoundByNormalizedEmailQueryAsync(
                email: request.Email,
                cancellation: cancellationToken
            );

        // Responds if email is exist.
        if (isEmailExist)
        {
            return new() { StatusCode = RegisterAsUserResponseStatusCode.EMAIL_IS_EXIST, };
        }

        // Check username is registered by other users.
        var isUsernameExist =
            await _unitOfWork.AuthFeature.RegisterAsUserRepository.IsUserFoundByNormalizedUsernameQueryAsync(
                username: request.Username,
                cancellation: cancellationToken
            );

        // Responds if username is exist.
        if (isUsernameExist)
        {
            return new() { StatusCode = RegisterAsUserResponseStatusCode.USERNAME_IS_EXIST };
        }

        User newUser =
            new()
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                Email = request.Email
            };

        // Validation password.
        var isPasswordValid = await ValidateUserPasswordAsync(
            password: request.Password,
            newUser: newUser
        );

        // Responds if password is not valid.
        if (!isPasswordValid)
        {
            return new() { StatusCode = RegisterAsUserResponseStatusCode.PASSWORD_IS_NOT_VAID, };
        }

        // Init user information.
        InitFillingUser(newUser: newUser);

        var newCart = InitFillingCart(newUser: newUser);

        // Create user and role to database
        var dbResult =
            await _unitOfWork.AuthFeature.RegisterAsUserRepository.CreateUserAndAddUserRoleCommandAsync(
                newUser: newUser,
                userManager: _userManager,
                userPassword: request.Password,
                userRole: "user",
                newCart: newCart,
                cancellationToken: cancellationToken
            );

        // Responds if user cannot create successfully.
        if (!dbResult)
        {
            return new() { StatusCode = RegisterAsUserResponseStatusCode.DATABASE_OPERATION_FAIL, };
        }

        // Send account creation confirmation mail to user.
        var mailSendingResult = await SendingConfirmationMailToUserAsync(
            newUser: newUser,
            cancellationToken: cancellationToken
        );

        // Responds if cannot send mail.
        if (!mailSendingResult)
        {
            return new()
            {
                StatusCode = RegisterAsUserResponseStatusCode.SENDING_USER_CONFIRMATION_MAIL_FAIL,
            };
        }

        // Response successfully.
        return new RegisterAsUserResponse()
        {
            StatusCode = RegisterAsUserResponseStatusCode.OPERATION_SUCCESS
        };
    }

    /// <summary>
    ///     Sends a confirmation mail to a newly created user.
    /// </summary>
    /// <param name="newUser">
    ///     The newly created user.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token.
    /// </param>
    /// <returns>
    ///     Returns a task that represents the asynchronous operation. The task result
    ///     contains a value indicating whether the mail was sent successfully.
    /// </returns>
    private async Task<bool> SendingConfirmationMailToUserAsync(
        User newUser,
        CancellationToken cancellationToken
    )
    {
        const string RegistrationConfirmEmailRoutePatuh = "api/auth/sign-up/confirm-email?token=";

        // Init main account creation confirmed email token.
        var accountCreationConfirmEmailToken_1 =
            await _userManager.GenerateEmailConfirmationTokenAsync(user: newUser);

        // Convert to utf-8 byte array.
        var accountCreationConfirmEmailTokenAsBytes_1 = Encoding.UTF8.GetBytes(
            s: $"{accountCreationConfirmEmailToken_1}<token/>{newUser.Id}"
        );

        // Convert to base 64 format.
        var accountCreateConfirmEmailTokenAsBase64_1 = WebEncoders.Base64UrlEncode(
            input: accountCreationConfirmEmailTokenAsBytes_1
        );

        // Init secondary account creation confirmed email token.
        var accountCreationConfirmEmailToken_2 =
            await _userManager.GenerateEmailConfirmationTokenAsync(user: newUser);

        // Convert to utf-8 byte array.
        var accountCreationConfirmEmailTokenAsBytes_2 = Encoding.UTF8.GetBytes(
            s: $"{accountCreationConfirmEmailToken_2}<token/>{newUser.Id}"
        );

        // Convert to base 64 format.
        var accountCreationConfirmEmailTokenAsBase64_2 = WebEncoders.Base64UrlEncode(
            input: accountCreationConfirmEmailTokenAsBytes_2
        );

        // Init new mail for account confirmation.
        var mailContent = await _sendingMailHandler.GetUserAccountConfirmationMailContentAsync(
            to: newUser.Email,
            subject: "Confirm account registration",
            mainVerifyLink: RegistrationConfirmEmailRoutePatuh
                + accountCreateConfirmEmailTokenAsBase64_1,
            alternativeVerifyLink: accountCreationConfirmEmailTokenAsBase64_2,
            cancellationToken: cancellationToken
        );

        // Send mail to user.
        return await _sendingMailHandler.SendAsync(
            mailContent: mailContent,
            cancellationToken: cancellationToken
        );
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
    private async Task<bool> ValidateUserPasswordAsync(string password, User newUser)
    {
        IdentityResult result = default;
        foreach (var validator in _userManager.PasswordValidators)
        {
            result = await validator.ValidateAsync(
                manager: _userManager,
                user: newUser,
                password: password
            );
        }

        if (Equals(objA: result, objB: default))
        {
            return false;
        }

        return result.Succeeded;
    }

    /// <summary>
    ///     Finishes filling the user with default
    ///     values for the newly created user.
    /// </summary>
    /// <param name="newUser">
    ///     The newly created user.
    /// </param>
    /// <param name="userJoiningStatusId">
    ///     The status of the user's joining.
    /// </param>
    private void InitFillingUser(User newUser)
    {
        newUser.UserDetail = new UserDetail()
        {
            UserId = newUser.Id,
            FirstName = string.Empty,
            LastName = string.Empty,
            AddressId = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            AvatarUrl = _defaultUserAvatarAsUrlHandler.Get(),
            Gender = string.Empty,
            DateOfBirth = CommonConstant.MIN_DATE_TIME,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = newUser.Id,
        };
    }

    /// <summary>
    ///     Finishes filling the cart with default
    ///     values for the newly created user.
    /// </summary>
    /// <param name="newUser">
    ///     The newly created user.
    /// </param>
    /// <param name="userJoiningStatusId">
    ///     The status of the user's joining.
    /// </param>
    private Cart InitFillingCart(User newUser)
    {
        return new()
        {
            UserId = newUser.Id,
            Id = Guid.NewGuid(),
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = newUser.Id,
        };
    }
}
