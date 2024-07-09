using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Users.GetAllUsersTemporarilyRemovedById;

/// <summary>
///     GetAllUsersTemporarilyRemovedById Handler
/// </summary>
public class GetAllUsersTemporarilyRemovedByIdHandler
    : IFeatureHandler<
        GetAllUsersTemporarilyRemovedByIdRequest,
        GetAllUsersTemporarilyRemovedByIdResponse
    >
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersTemporarilyRemovedByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
    public async Task<GetAllUsersTemporarilyRemovedByIdResponse> HandlerAsync(
        GetAllUsersTemporarilyRemovedByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get all users.
        var users =
            await _unitOfWork.UserFeature.GetAllUsersTemporarilyRemovedByIdRepository.FindAllUsersTemporarilyRemovedByIdQueryAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Count all the users.
        var countUser =
            await _unitOfWork.UserFeature.GetAllUsersTemporarilyRemovedByIdRepository.CountAllTemporarilyRemovedUserQueryAsync(
                cancellationToken: cancellationToken
            );

        // Response successfully.
        return new GetAllUsersTemporarilyRemovedByIdResponse()
        {
            StatusCode = GetAllUsersTemporarilyRemovedByIdResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Users =
                    new PaginationResponse<GetAllUsersTemporarilyRemovedByIdResponse.Body.User>()
                    {
                        Contents = users.Select(
                            user => new GetAllUsersTemporarilyRemovedByIdResponse.Body.User()
                            {
                                Id = user.UserId,
                                FullName = $"{user.FirstName} {user.LastName}",
                                AvatarUrl = user.AvatarUrl,
                                Email = user.User.Email,
                                Gender = user.Gender,
                                Username = user.User.UserName
                            }
                        ),
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                        TotalPages = (int)Math.Ceiling((double)countUser / request.PageSize)
                    }
            }
        };
    }
}
