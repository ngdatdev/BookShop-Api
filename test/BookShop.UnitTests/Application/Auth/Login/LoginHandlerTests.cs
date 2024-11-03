using System.Security.Claims;
using AutoFixture;
using BookShop.Application.Features.Auth.Login;
using BookShop.Application.Shared.Authentication.Jwt;
using BookShop.Data.Features.UnitOfWork;
using BookShop.Data.Shared.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BookShop.UnitTests.Application.Auth.Login;

/// <summary>
///     Tests for <see cref="LoginHandler"/>
/// </summary>
public class LoginHandlerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<SignInManager<User>> _signInManagerMock;
    private readonly Mock<IRefreshTokenHandler> _refreshTokenHandlerMock;
    private readonly Mock<IAccessTokenHandler> _accessTokenHandlerMock;
    private readonly LoginHandler _loginHandler;

    public LoginHandlerTests()
    {
        _fixture = new Fixture();

        _fixture
            .Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));

        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        // Setup mocks
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );
        _signInManagerMock = new Mock<SignInManager<User>>(
            _userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<User>>(),
            null,
            null,
            null,
            null
        );
        _refreshTokenHandlerMock = new Mock<IRefreshTokenHandler>();
        _accessTokenHandlerMock = new Mock<IAccessTokenHandler>();

        // Initialize LoginHandler with mocks
        _loginHandler = new LoginHandler(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _refreshTokenHandlerMock.Object,
            _accessTokenHandlerMock.Object
        );
    }

    [Fact]
    public async Task HandlerAsync_ShouldReturnUserNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var loginRequest = _fixture.Create<LoginRequest>();
        _userManagerMock
            .Setup(um => um.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null);

        // Act
        var result = await _loginHandler.HandlerAsync(loginRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(LoginResponseStatusCode.USER_IS_NOT_FOUND);
    }

    [Fact]
    public async Task HandlerAsync_ShouldReturnEmailNotConfirmed_WhenEmailIsNotConfirmed()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var loginRequest = _fixture.Create<LoginRequest>();

        _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(false);

        // Act
        var result = await _loginHandler.HandlerAsync(loginRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(LoginResponseStatusCode.EMAIL_IS_NOT_CONFIRMED);
    }

    [Fact]
    public async Task HandlerAsync_ShouldReturnIncorrectPassword_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var loginRequest = _fixture.Create<LoginRequest>();

        _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
        _signInManagerMock
            .Setup(sm => sm.CheckPasswordSignInAsync(user, loginRequest.Password, true))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var result = await _loginHandler.HandlerAsync(loginRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(LoginResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT);
    }

    [Fact]
    public async Task HandlerAsync_ShouldReturnUserLockedOut_WhenUserIsLockedOut()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var loginRequest = _fixture.Create<LoginRequest>();

        _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
        _signInManagerMock
            .Setup(sm => sm.CheckPasswordSignInAsync(user, loginRequest.Password, true))
            .ReturnsAsync(SignInResult.LockedOut);

        // Act
        var result = await _loginHandler.HandlerAsync(loginRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(LoginResponseStatusCode.USER_IS_LOCKED_OUT);
    }

    [Fact]
    public async Task HandlerAsync_ShouldReturnOperationSuccess_WhenLoginIsSuccessful()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var loginRequest = _fixture.Create<LoginRequest>();
        var userRoles = new List<string> { "user" };
        var userDetail = new UserDetail
        {
            AvatarUrl = "http://example.com/avatar.png",
            FirstName = "John",
            LastName = "Doe"
        };

        _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
        _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(userRoles);

        _signInManagerMock
            .Setup(sm => sm.CheckPasswordSignInAsync(user, loginRequest.Password, true))
            .ReturnsAsync(SignInResult.Success);

        _unitOfWorkMock
            .Setup(uow =>
                uow.AuthFeature.LoginRepository.IsUserTemporarilyRemovedQueryAsync(
                    user.Id,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

        _unitOfWorkMock
            .Setup(uow =>
                uow.AuthFeature.LoginRepository.CreateRefreshTokenCommandAsync(
                    It.IsAny<RefreshToken>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

        _unitOfWorkMock
            .Setup(uow =>
                uow.AuthFeature.LoginRepository.GetUserDetailByUserIdQueryAsync(
                    user.Id,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(userDetail);

        _refreshTokenHandlerMock
            .Setup(rt => rt.Generate(It.IsAny<int>()))
            .Returns("dummy-refresh-token");
        _accessTokenHandlerMock
            .Setup(at => at.GenerateSigningToken(It.IsAny<IEnumerable<Claim>>()))
            .Returns("dummy-access-token");

        // Act
        var result = await _loginHandler.HandlerAsync(loginRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(LoginResponseStatusCode.OPERATION_SUCCESS);
        result.ResponseBody.AccessToken.Should().Be("dummy-access-token");
        result.ResponseBody.RefreshToken.Should().Be("dummy-refresh-token");
        result.ResponseBody.User.Email.Should().Be(user.Email);
        result.ResponseBody.User.AvatarUrl.Should().Be(userDetail.AvatarUrl);
        result.ResponseBody.User.FirstName.Should().Be(userDetail.FirstName);
        result.ResponseBody.User.LastName.Should().Be(userDetail.LastName);
    }
}
