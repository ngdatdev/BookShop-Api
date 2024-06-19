using BookShop.Application.Shared.FileObjectStorage;
using BookShop.Application.Shared.FileObjectStorages;
using BookShop.Configuration.Infrastructure.Cloudinary;
using BookShop.ImageCloudinary.Image;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.ImageCloudinary;

/// <summary>
///     Configure services for this layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Entry to configuring multiple services.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    public static void ConfigCloudinaryImageStorage(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddSingleton<
            IDefaultUserAvatarAsUrlHandler,
            DefaultUserAvatarAsUrlSourceHandler
        >();

        services.AddSingleton<ICloudinaryStorageHandler, CloudinaryStorageHandler>();

        services.AddSingleton(provider =>
        {
            var cloudinaryOption = configuration.GetSection("Cloudinary").Get<CloudinaryOption>();

            var account = new Account(
                cloudinaryOption.CloudName,
                cloudinaryOption.ApiKey,
                cloudinaryOption.ApiSecret
            );
            var cloudinary = new Cloudinary(account) { Api = { Secure = true, } };

            return cloudinary;
        });
    }
}
