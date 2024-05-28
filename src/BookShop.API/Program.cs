using System.Text;
using BookShop.API.Middleware;
using BookShop.DataAccess;
using BookShop.DataAccess.Data;
using BookShop.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
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

var app = builder.Build();

// Data Seeding
// Data seeding.
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

app.UseAuthorization();
app.UseExceptionHandler();
app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
