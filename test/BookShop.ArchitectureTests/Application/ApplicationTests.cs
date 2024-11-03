using System.Reflection;
using BookShop.Application;
using BookShop.Application.Shared.Features;
using BookShop.Data.Shared.Entities.Base;
using FluentAssertions;
using NetArchTest.Rules;

namespace BookShop.ArchitectureTests.Application;

/// <summary>
///     Test for Application layer.
/// </summary>
public class ApplicationTests
{
    private static readonly Assembly ApplicationAssembly = typeof(DependencyInjection).Assembly;
    private static readonly Assembly DomainAssembly = typeof(IBaseEntity).Assembly;

    [Fact]
    public void Application_Should_Not_Reference_To_Other_Layer()
    {
        string[] dependenciesProject =
        [
            "BookShop.API",
            "BookShop.Smtp",
            "BookShop.PayOSGateway",
            "BookShop.Redis",
            "BookShop.PostgresSql"
        ];
        // Arrange
        var result = Types
            .InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(dependenciesProject)
            .GetResult();

        // Act & Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Implement_Handler_Should_Have_HandlerName()
    {
        // Arrange
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IFeatureHandler<,>))
            .Should()
            .HaveNameEndingWith("Handler")
            .GetResult();

        // Act & Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Implement_Request_Should_Have_RequestName()
    {
        // Arrange
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IFeatureRequest<>))
            .Should()
            .HaveNameEndingWith("Request")
            .GetResult();

        // Act & Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Implement_Response_Should_Have_ResponseName()
    {
        // Arrange
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IFeatureResponse))
            .Should()
            .HaveNameEndingWith("Response")
            .GetResult();

        // Act & Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
