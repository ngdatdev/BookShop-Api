using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace BookShop.ArchitectureTests.Infrastructure;

/// <summary>
///     Infrastructures Tests
/// </summary>
public class InfrastructureTests
{
    private static IEnumerable<Assembly> Assemblies =>
        new List<Assembly>
        {
            typeof(JsonWebToken.DependencyInjection).Assembly,
            typeof(MediatrCustom.DependencyInjection).Assembly,
            typeof(Smtp.DependencyInjection).Assembly,
            typeof(PayOSGateway.DependencyInjection).Assembly,
            typeof(PostgresSql.DependencyInjection).Assembly,
            typeof(Redis.DependencyInjection).Assembly
        };

    [Fact]
    public void Infrastructure_Should_Be_Referenced_Application()
    {
        // Arrange
        var result = Types
            .InAssemblies(Assemblies)
            .Should()
            .HaveDependencyOn("BookShop.Application")
            .GetResult();

        // Act & Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
