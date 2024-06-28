using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Data.Features.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Features.Users.RestoreUserById;

/// <summary>
///     RestoreUserById Handler
/// </summary>
public class RestoreUserByIdHandler
    : IFeatureHandler<RestoreUserByIdRequest, RestoreUserByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RestoreUserByIdHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
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
    public async Task<RestoreUserByIdResponse> HandlerAsync(
        RestoreUserByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check user id is exist in database.
        var isUserIdFound =
            await _unitOfWork.UserFeature.RestoreUserByIdRepository.IsUserFoundByIdQueryAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Responds if user is not found.
        if (!isUserIdFound)
        {
            return new() { StatusCode = RestoreUserByIdResponseStatusCode.USER_ID_IS_NOT_FOUND };
        }
        // Check user is temporarily removed by id.
        var isUserTemporarilyRemoved =
            await _unitOfWork.UserFeature.RestoreUserByIdRepository.IsUserTemporarilyRemovedByIdQueryAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Responds if users is temporarily removed.
        if (!isUserTemporarilyRemoved)
        {
            return new()
            {
                StatusCode = RestoreUserByIdResponseStatusCode.USER_IS_ALREADY_TEMPORARILY_REMOVED
            };
        }

        // Remove user temporarily command.
        var dbResult =
            await _unitOfWork.UserFeature.RestoreUserByIdRepository.RestoreUserByIdCommandAsync(
                userId: request.UserId,
                cancellationToken: cancellationToken
            );

        // Responds if database transaction fasle.
        if (!dbResult)
        {
            return new() { StatusCode = RestoreUserByIdResponseStatusCode.DATABASE_OPERATION_FAIL };
        }

        // Response successfully.
        return new RestoreUserByIdResponse()
        {
            StatusCode = RestoreUserByIdResponseStatusCode.OPERATION_SUCCESS,
        };
    }
}
