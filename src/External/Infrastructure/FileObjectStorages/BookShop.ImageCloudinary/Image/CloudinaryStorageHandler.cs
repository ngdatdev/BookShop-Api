using BookShop.Application.Shared.FileObjectStorages;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BookShop.ImageCloudinary.Image;

/// <summary>
///     Implementation of cloudinary storage handler.
/// </summary>
public sealed class CloudinaryStorageHandler : ICloudinaryStorageHandler
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryStorageHandler(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> UploadPhotoAsync(IFormFile image, CancellationToken cancellationToken)
    {
        if (image == null || image.Length == 0)
        {
            return string.Empty;
        }

        using var stream = image.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription() { FileName = Guid.NewGuid().ToString(), Stream = stream },
            Overwrite = true
        };

        ImageUploadResult uploadResult;
        uploadResult = await _cloudinary.UploadAsync(
            parameters: uploadParams,
            cancellationToken: cancellationToken
        );
        if (uploadResult.Error != null)
        {
            return String.Empty;
        }

        return uploadResult.Url.ToString();
    }
}
