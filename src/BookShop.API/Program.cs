using System;
using System.Text;
using System.Threading;
using BookShop.API;
using BookShop.API.Middleware;
using BookShop.Application;
using BookShop.DataAccess;
using BookShop.DataAccess.Data;
using BookShop.DataAccess.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;

// Default setting.
Console.OutputEncoding = Encoding.UTF8;
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.ConfigureSqlRelationalDatabase(configuration: configuration);
services.ConfigApplication(configuration: configuration);
services.ConfigWebAPI(configuration: configuration);

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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
