using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NetArchTest.Rules;

namespace BookShop.ArchitectureTests.WebAPI;

/// <summary>
///     This class is used to test WebAPI
/// </summary>
public class WebAPITests
{
    private static readonly Assembly APIAssebly = typeof(API.DependencyInjection).Assembly;

    public void Controller_Should_Have_Tail_Controller_Name()
    {
        // Arrange
        var result = Types
            .InAssembly(APIAssebly)
            .That()
            .ResideInNamespace("BookShop.API.Controllers")
            .Should()
            .HaveNameEndingWith("Controller")
            .GetResult();

        // Assert & Act
        result.IsSuccessful.Should().BeTrue();
    }
}
