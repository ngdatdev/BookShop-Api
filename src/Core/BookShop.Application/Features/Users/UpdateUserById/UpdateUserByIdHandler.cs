using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Common;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Application.Features.Users.UpdateUserById;

/// <summary>
///     UpdateUserById Handler
/// </summary>
public class UpdateUserByIdHandler : IFeatureHandler<UpdateUserByIdRequest, UpdateUserByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;

    public UpdateUserByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor contextAccessor,
        ICloudinaryStorageHandler cloudinaryStorageHandler
    )
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
        _cloudinaryStorageHandler = cloudinaryStorageHandler;
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
    public async Task<UpdateUserByIdResponse> HandlerAsync(
        UpdateUserByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get userId from sub type jwt
        var userId = Guid.Parse(
            _contextAccessor.HttpContext.User.FindFirstValue(claimType: JwtRegisteredClaimNames.Sub)
        );

        // Found user by userId
        var foundUser =
            await _unitOfWork.UserFeature.UpdateUserByIdRepository.FindUserByIdQueryAsync(
                userId: userId,
                cancellationToken: cancellationToken
            );

        // Responds if userId is not found
        if (Equals(objA: foundUser, objB: default))
        {
            return new UpdateUserByIdResponse()
            {
                StatusCode = UpdateUserByIdResponseStatusCode.USER_IS_NOT_FOUND
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
                StatusCode = UpdateUserByIdResponseStatusCode.USER_IS_TEMPORARILY_REMOVED
            };
        }

        // Split address information.
        var addressInfo = request.Address.Split(separator: "<token/>");

        // Find address id.
        Guid addressId = default;
        try
        {
            addressId =
                await _unitOfWork.UserFeature.UpdateUserByIdRepository.FindAddressIdFoundByNameQueryAsync(
                    ward: addressInfo[0],
                    district: addressInfo[1],
                    province: addressInfo[2],
                    cancellationToken: cancellationToken
                );
        }
        catch
        {
            return new()
            {
                StatusCode = UpdateUserByIdResponseStatusCode.ADDRESS_IS_NOT_CORRECT_FORMAT
            };
        }

        // Create address if it is not exist in database.
        if (Equals(objA: addressId, objB: Guid.Empty))
        {
            addressId = Guid.NewGuid();
            var dbAddressResult =
                await _unitOfWork.UserFeature.UpdateUserByIdRepository.CreateAddressCommandAsync(
                    address: MapperToAddress(
                        addressId: addressId,
                        ward: addressInfo[0],
                        district: addressInfo[1],
                        province: addressInfo[2],
                        userId: userId
                    ),
                    cancellationToken: cancellationToken
                );

            if (!dbAddressResult)
            {
                return new()
                {
                    StatusCode = UpdateUserByIdResponseStatusCode.DATABASE_OPERATION_FAIL
                };
            }
        }

        // If New AvatarUrl is not update.
        var uploadAvatarUrl = string.Empty;
        if (!Equals(objA: request.NewAvatarUrl, objB: default))
        {
            uploadAvatarUrl = await _cloudinaryStorageHandler.UploadPhotoAsync(
                formFile: request.NewAvatarUrl,
                cancellationToken: cancellationToken
            );

            // Responds if upload is fail.
            if (string.IsNullOrEmpty(value: uploadAvatarUrl))
            {
                return new() { StatusCode = UpdateUserByIdResponseStatusCode.UPLOAD_IMAGE_FAIL };
            }
        }

        // Mapper to user entity.
        var updateUser = MapperToUser(
            updateUserByIdRequest: request,
            avatarUrl: uploadAvatarUrl.IsNullOrEmpty() ? request.OldAvatarUrl : uploadAvatarUrl,
            userDetail: foundUser.UserDetail,
            addressId: addressId
        );

        // Update user by id.
        var dbUpdateUserResult =
            await _unitOfWork.UserFeature.UpdateUserByIdRepository.UpdateUserByIdCommandAsync(
                updateUser: updateUser,
                currentUser: foundUser,
                cancellationToken: cancellationToken
            );

        // Responds if updating user is not successfull.
        if (!dbUpdateUserResult)
        {
            await _cloudinaryStorageHandler.DeletePhotoAsync(imageUrl: uploadAvatarUrl);

            return new() { StatusCode = UpdateUserByIdResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new UpdateUserByIdResponse()
        {
            StatusCode = UpdateUserByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }

    private Data.Shared.Entities.User MapperToUser(
        UpdateUserByIdRequest updateUserByIdRequest,
        Guid addressId,
        string avatarUrl,
        Data.Shared.Entities.UserDetail userDetail
    )
    {
        return new()
        {
            Id = userDetail.UserId,
            Email = updateUserByIdRequest.Email,
            UserName = updateUserByIdRequest.Username,
            PhoneNumber = updateUserByIdRequest.NumberPhone,
            UserDetail = new()
            {
                UserId = userDetail.UserId,
                FirstName = updateUserByIdRequest.FirstName,
                LastName = updateUserByIdRequest.LastName,
                AvatarUrl = avatarUrl,
                Gender = updateUserByIdRequest.Gender,
                DateOfBirth = updateUserByIdRequest.DateOfBirth,
                AddressId = addressId,
                CreatedAt = userDetail.CreatedAt,
                CreatedBy = userDetail.CreatedBy,
                RemovedAt = userDetail.RemovedAt,
                RemovedBy = userDetail.RemovedBy,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = userDetail.UserId,
            }
        };
    }

    private static Data.Shared.Entities.Address MapperToAddress(
        Guid addressId,
        string ward,
        string district,
        string province,
        Guid userId
    )
    {
        return new()
        {
            Id = addressId,
            Ward = ward,
            District = district,
            Province = province,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId,
            RemovedAt = CommonConstant.MIN_DATE_TIME,
            RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = CommonConstant.MIN_DATE_TIME,
            UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
        };
    }
}
