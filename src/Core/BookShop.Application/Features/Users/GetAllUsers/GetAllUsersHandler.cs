using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.Shared.Features;
using BookShop.Application.Shared.Pagination;
using BookShop.Data.Features.UnitOfWork;

namespace BookShop.Application.Features.Users.GetAllUsers;

/// <summary>
///     GetAllUsers Handler
/// </summary>
public class GetAllUsersHandler : IFeatureHandler<GetAllUsersRequest, GetAllUsersResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersHandler(IUnitOfWork unitOfWork)
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
    public async Task<GetAllUsersResponse> HandlerAsync(
        GetAllUsersRequest request,
        CancellationToken cancellationToken
    )
    {
        // Get all users.
        var users =
            await _unitOfWork.UserFeature.GetAllUsersRepository.FindUserDetailByIdQueryAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken
            );

        // Count all the users.
        var countUser = await _unitOfWork.UserFeature.GetAllUsersRepository.CountAllUserQueryAsync(
            cancellationToken: cancellationToken
        );

        // Response successfully.
        return new GetAllUsersResponse()
        {
            StatusCode = GetAllUsersResponseStatusCode.OPERATION_SUCCESS,
            ResponseBody = new()
            {
                Users = new PaginationResponse<GetAllUsersResponse.Body.User>()
                {
                    Contents = users.Select(user => new GetAllUsersResponse.Body.User()
                    {
                        Id = user.UserId,
                        FullName = $"{user.FirstName} {user.LastName}",
                        AvatarUrl = user.AvatarUrl,
                        Email = user.User.Email,
                        Gender = user.Gender,
                        Username = user.User.UserName
                    }),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling((double)countUser / request.PageSize)
                }
            }
        };
    }
}
