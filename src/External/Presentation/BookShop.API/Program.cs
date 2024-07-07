using System;
using System.Text;
using System.Threading;
using BookShop.API;
using BookShop.API.Shared.Middlewares;
using BookShop.Application;
using BookShop.Data.Shared.Entities;
using BookShop.ImageCloudinary;
using BookShop.JsonWebToken;
using BookShop.MediatrCustom;
using BookShop.PostgresSql;
using BookShop.PostgresSql.Data;
using BookShop.Redis;
using BookShop.Smtp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;

// Default setting.
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
Console.OutputEncoding = Encoding.UTF8;
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.ConfigurePostgreSqlDatabase(configuration: configuration);
services.ConfigApplication();
services.ConfigWebAPI(configuration: configuration);
services.ConfigMediatorService();
services.ConfigureJwtIdentityService();
services.AddRedisCachingDatabase(configuration: configuration);
services.ConfigGoogleSmtpMailNotification(configuration: configuration);
services.ConfigCloudinaryImageStorage(configuration: configuration);
var app = builder.Build();

// Data Seeding
await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookShopContext>();

    // Can database be connected.
    var canConnect = await context.Database.CanConnectAsync();

    // Database cannot be connected.
    if (!canConnect)
    {
        throw new HostAbortedException(message: "Cannot connect database.");
    }

    // Try seed data.
    var seedResult = await EntityDataSeeding.SeedAsync(
        context: context,
        userManager: scope.ServiceProvider.GetRequiredService<UserManager<User>>(),
        roleManager: scope.ServiceProvider.GetRequiredService<RoleManager<Role>>(),
        cancellationToken: CancellationToken.None
    );

    // Data cannot be seed.
    if (!seedResult)
    {
        throw new HostAbortedException(message: "Database seeding is false.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction: options =>
    {
        options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
        options.RoutePrefix = string.Empty;
        options.DefaultModelsExpandDepth(depth: -1);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
