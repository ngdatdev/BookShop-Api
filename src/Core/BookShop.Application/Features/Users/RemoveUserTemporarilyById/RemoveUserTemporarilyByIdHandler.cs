using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BookShop.Application.Features.Users.RemoveUserTemporarilyById;

/// <summary>
///     RemoveUserTemporarilyById Handler
/// </summary>
public class RemoveUserTemporarilyByIdHandler
    : IFeatureHandler<RemoveUserTemporarilyByIdRequest, RemoveUserTemporarilyByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveUserTemporarilyByIdHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
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
    public async Task<RemoveUserTemporarilyByIdResponse> HandlerAsync(
        RemoveUserTemporarilyByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check user id is exist in database.
        var isUserIdFound =
            await _unitOfWork.UserFeature.RemoveUserTemporarilyByIdRepository.IsUserFoundByIdQueryAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Responds if user is not found.
        if (!isUserIdFound)
        {
            return new()
            {
                StatusCode = RemoveUserTemporarilyByIdResponseStatusCode.USER_ID_IS_NOT_FOUND
            };
        }
        // Check user is temporarily removed by id.
        var isUserTemporarilyRemoved =
            await _unitOfWork.UserFeature.RemoveUserTemporarilyByIdRepository.IsUserTemporarilyRemovedByIdQueryAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Responds if users is temporarily removed.
        if (isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode =
                    RemoveUserTemporarilyByIdResponseStatusCode.USER_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Find userId in claim jwt.
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            claimType: JwtRegisteredClaimNames.Sub
        );

        // Remove user temporarily command.
        var dbResult =
            await _unitOfWork.UserFeature.RemoveUserTemporarilyByIdRepository.RemoveUserTemporarilyByIdCommandAsync(
                userId: request.UserId,
                removedAt: DateTime.UtcNow,
                removedBy: Guid.Parse(input: userId),
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RemoveUserTemporarilyByIdResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        // Response successfully.
        return new RemoveUserTemporarilyByIdResponse()
        {
            StatusCode = RemoveUserTemporarilyByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
