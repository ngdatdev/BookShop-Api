using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookShop.Application.Shared.FileObjectStorages;

/// <summary>
///     Represent the handler for cloudinar storage service.
/// </summary>
public interface ICloudinaryStorageHandler
{
    /// <summary>
    ///     Handles the upload photo async with cloudinary.
    /// </summary>
    /// <param name="formFile">
    //      The request contain image file.
    //  </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     The response boolean.
    ///</returns>
    Task<string> UploadPhotoAsync(IFormFile formFile);
}
