using System;
using System.IO;
using System.Reflection;
using BookShop.Configuration.Presentation.WebApi.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BookShop.API.Shared.ServiceConfigs;

/// <summary>
///     Swagger services config.
/// </summary>
internal static class SwaggerServiceConfig
{
    /// <summary>
    ///     Configure the swagger service.
    /// </summary>
    /// <param name="services">
    ///     Service container.
    /// </param>
    /// <param name="configuration">
    ///     Load configuration for configuration
    ///     file (appsetting).
    /// </param>
    internal static void ConfigureSwagger(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddSwaggerGen(setupAction: config =>
        {
            var option = configuration
                .GetRequiredSection(key: "Swagger")
                .GetRequiredSection(key: "Swashbuckle")
                .Get<SwashbuckleOption>();

            config.SwaggerDoc(
                name: option.Doc.Name,
                info: new()
                {
                    Version = option.Doc.Info.Version,
                    Title = option.Doc.Info.Title,
                    Description = option.Doc.Info.Description,
                    Contact = new()
                    {
                        Name = option.Doc.Info.Contact.Name,
                        Email = option.Doc.Info.Contact.Email,
                    },
                    License = new()
                    {
                        Name = option.Doc.Info.License.Name,
                        Url = new(uriString: option.Doc.Info.License.Url)
                    }
                }
            );

            config.AddSecurityDefinition(
                name: option.Security.Definition.Name,
                securityScheme: new()
                {
                    Description = option.Security.Definition.SecurityScheme.Description,
                    Name = option.Security.Definition.Name,
                    In = (ParameterLocation)
                        Enum.ToObject(
                            enumType: typeof(ParameterLocation),
                            value: option.Security.Definition.SecurityScheme.In
                        ),
                    Type = (SecuritySchemeType)
                        Enum.ToObject(
                            enumType: typeof(SecuritySchemeType),
                            value: option.Security.Definition.SecurityScheme.Type
                        ),
                    Scheme = option.Security.Definition.SecurityScheme.Scheme
                }
            );

            config.AddSecurityRequirement(
                securityRequirement: new()
                {
                    {
                        new()
                        {
                            Reference = new()
                            {
                                Type = (ReferenceType)
                                    Enum.ToObject(
                                        enumType: typeof(ReferenceType),
                                        value: option
                                            .Security
                                            .Requirement
                                            .OpenApiSecurityScheme
                                            .Reference
                                            .Type
                                    ),
                                Id = option.Security.Requirement.OpenApiSecurityScheme.Reference.Id
                            },
                            Scheme = option.Security.Requirement.OpenApiSecurityScheme.Scheme,
                            Name = option.Security.Requirement.OpenApiSecurityScheme.Name,
                            In = (ParameterLocation)
                                Enum.ToObject(
                                    enumType: typeof(ParameterLocation),
                                    value: option.Security.Requirement.OpenApiSecurityScheme.In
                                ),
                        },
                        option.Security.Requirement.Values
                    }
                }
            );
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            var xmlFilePath = Path.Combine(path1: AppContext.BaseDirectory, path2: xmlFilename);

            config.IncludeXmlComments(filePath: xmlFilePath);
        });
    }
}
