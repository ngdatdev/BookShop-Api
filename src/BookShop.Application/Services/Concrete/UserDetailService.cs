using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.DTOs.Response;
using BookShop.Application.Services.Interface;
using BookShop.DataAccess.UnitOfWork;

namespace BookShop.Application.Services.Concrete;

public class UserDetailService : IUserDetailService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserDetailService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserDetailResponse>> GetAllUserDetails(
        CancellationToken cancellationToken
    )
    {
        var userDetails = await _unitOfWork.UserDetailRepository.GetAllAsync(
            cancellationToken: cancellationToken
        );
        return userDetails.Select(userDetail => new UserDetailResponse
        {
            FullName = $"{userDetail.FirstName} {userDetail.LastName}",
            AvtUrl = userDetail.AvatarUrl,
        });
    }
}
