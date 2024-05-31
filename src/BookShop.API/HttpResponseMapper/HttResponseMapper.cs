using BookShop.API.HttpResponseMapper.ErrorApiResponse;
using BookShop.API.HttpResponseMapper.SuccessApiResponse;
using BookShop.Application.ResponseEntity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.HttpResponseMapper;

/// <summary>
///     extension methods to response api.
/// </summary>
internal static class HttResponseMapper
{
    private static ErrorHttpResponseManager _errorHttpResponseManager;

    internal static IActionResult ToApiResponse<T>(ResponseEntity<T> responseDTO)
        where T : class
    {
        switch (responseDTO.AppCode)
        {
            case ResponseAppCode.OPERATION_SUCCESS:
                return SuccessResponse(responseDTO.Body);

            default:
                return ErrorResponse(responseDTO.AppCode);
        }
    }

    private static IActionResult SuccessResponse<T>(T body)
        where T : class
    {
        var successResponse = new SuccessHttpResponse { Body = body };
        return new ObjectResult(successResponse) { StatusCode = successResponse.HttpCode };
    }

    private static IActionResult ErrorResponse(ResponseAppCode appCode)
    {
        _errorHttpResponseManager ??= new();
        var errorResponseFunc = _errorHttpResponseManager.Resolve(appCode);
        var errorResponse = errorResponseFunc();
        return new ObjectResult(errorResponse) { StatusCode = errorResponse.HttpCode };
    }
}
