using System.Reflection;
using BookShop.Data.Shared.Entities.Base;
using FluentAssertions;
using NetArchTest.Rules;

namespace BookShop.ArchitectureTests.Domain;

/// <summary>
///     Domain layer architecture tests.
/// </summary>
public class DomainTests
{
    private static readonly Assembly DomainAssembly = typeof(IBaseEntity).Assembly;

    [Fact]
    public void Domain_Should_Not_Depend_On_Application()
    {
        // Arrange
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .AreClasses()
            .Should()
            .NotHaveDependencyOn("BookShop.Application");

        // Act & Assert
        result.GetResult().IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Models_Should_Be_Public()
    {
        // Arrange
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IBaseEntity))
            .Should()
            .BePublic();

        // Act & Assert
        result.GetResult().IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Repository_Should_End_With_RepositoryName()
    {
        // Arrange
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .ResideInNamespace("BookShop.Data.Features.Repositories")
            .Should()
            .HaveNameEndingWith("Repository");

        // Act & Assert
        result.GetResult().IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Entities_Should_Be_Implement_IBaseEntity()
    {
        // Arrange
        var result = Types
            .InAssembly(DomainAssembly)
            .That()
            .AreClasses()
            .And()
            .ArePublic()
            .And()
            .ResideInNamespace("BookShop.Data.Shared.Entities")
            .Should()
            .ImplementInterface(typeof(IBaseEntity));

        // Act & Assert
        result.GetResult().IsSuccessful.Should().BeTrue();
    }
}
