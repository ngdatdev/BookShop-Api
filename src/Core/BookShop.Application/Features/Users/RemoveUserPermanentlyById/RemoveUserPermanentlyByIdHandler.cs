using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Users.RemoveUserPermanentlyById;

/// <summary>
///     RemoveUserPermanentlyById Handler
/// </summary>
public class RemoveUserPermanentlyByIdHandler
    : IFeatureHandler<RemoveUserPermanentlyByIdRequest, RemoveUserPermanentlyByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryStorageHandler _cloudinaryStorageHandler;

    public RemoveUserPermanentlyByIdHandler(
        IUnitOfWork unitOfWork,
        ICloudinaryStorageHandler cloudinaryStorageHandler
    )
    {
        _unitOfWork = unitOfWork;
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
    public async Task<RemoveUserPermanentlyByIdResponse> HandlerAsync(
        RemoveUserPermanentlyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check user id and get avatar url in database.
        var avatarUserFound =
            await _unitOfWork.UserFeature.RemoveUserPermanentlyByIdRepository.FindAvatarUserByIdQueryAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Responds if user is not found.
        if (string.IsNullOrEmpty(avatarUserFound))
        {
            return new()
            {
                StatusCode = RemoveUserPermanentlyByIdResponseStatusCode.USER_ID_IS_NOT_FOUND
            };
        }

        // Check user is temporarily removed by id.
        var isProductTemporarilyRemoved =
            await _unitOfWork.UserFeature.RemoveUserPermanentlyByIdRepository.IsUserTemporarilyRemovedByIdQueryAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Responds if user is temporarily removed.
        if (!isProductTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveUserPermanentlyByIdResponseStatusCode.USER_IS_NOT_TEMPORARILY_REMOVED
            };
        }

        // Remove product temporarily command.
        var dbResult =
            await _unitOfWork.UserFeature.RemoveUserPermanentlyByIdRepository.RemoveUserPermanentlyByIdCommandAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            await _cloudinaryStorageHandler.DeletePhotoAsync(imageUrl: avatarUserFound);
            return new()
            {
                StatusCode = RemoveUserPermanentlyByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveUserPermanentlyByIdResponse()
        {
            StatusCode = RemoveUserPermanentlyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
