// using System.Threading;
// using System.Threading.Tasks;
// using BookShop.API.HttpResponseMapper;
// using BookShop.Application.DTOs.Request;
// using BookShop.Application.Services.Interface;
// using Microsoft.AspNetCore.Mvc;

// namespace BookShop.API.Controllers;

// [ApiController]
// [Route("/auth")]
// public class AuthController : ControllerBase
// {
//     private readonly IAuthService _authService;

//     public AuthController(IAuthService authService)
//     {
//         _authService = authService;
//     }

//     [HttpPost("/login")]
//     public async Task<IActionResult> Login(
//         [FromBody] LoginRequest loginRequest,
//         CancellationToken cancellationToken
//     )
//     {
//         var loginResponse = await _authService.LoginAsync(
//             loginRequest: loginRequest,
//             cancellationToken: cancellationToken
//         );
//         return HttResponseMapper.ToApiResponse(loginResponse);
//     }
// }
