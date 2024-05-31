using System.Threading;
using System.Threading.Tasks;
using BookShop.Application.DTOs.Response;
using BookShop.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers;

[ApiController]
public class UserDetailController : ControllerBase
{
    private readonly IUserDetailService _userDetailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserDetailController(
        IUserDetailService userDetailService,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _userDetailService = userDetailService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("/user")]
    [Authorize(Policy = "VerifyUser")]
    public async Task<ActionResult<UserDetailResponse>> Get(CancellationToken cancellationToken)
    {
        var userDetail = await _userDetailService.GetAllUserDetails(
            cancellationToken: cancellationToken
        );
        return Ok(userDetail);
        
    }

    // [Authorize(AuthenticationSchemes = "Bearer")]
    // [RequireJwt]
    // [HttpGet("hehe")]
    // public async Task<ActionResult<UserDetailResponse>> GetA(CancellationToken cancellationToken)
    // {
    //     var userDetail = await _userDetailService.GetAllUserDetails(
    //         cancellationToken: cancellationToken
    //     );
    //     return Ok(userDetail);
    // }

    // [HttpGet("/kaka")]
    // public ActionResult<List<string>> GetB()
    // {
    //     throw new Exception("hehe");
    // }
}
