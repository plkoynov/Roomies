using Moq;
using Roomies.Application.Enums;
using Roomies.Application.Interfaces;
using Roomies.Application.Interfaces.Repositories;
using Roomies.Application.Interfaces.Services;
using Roomies.Application.Models;
using Roomies.Application.Services;
using Roomies.Domain.Entities;

namespace Roomies.Application.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userService = new UserService(_userRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenEmailAlreadyExists()
        {
            // Arrange
            var existingUser = new User { Email = "test@example.com" };
            _userRepositoryMock.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var registerUserDto = new RegisterUserDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!"
            };

            // Act
            var result = await _userService.RegisterUserAsync(registerUserDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorCodes.UserAlreadyExists, result.Error);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnSuccess_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.EmailExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var registerUserDto = new RegisterUserDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!",
                TokenSecret = "SomeReallyReallyReallyReallyLongTokenSecret"
            };

            // Act
            var result = await _userService.RegisterUserAsync(registerUserDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.IsType<RegisterUserResult>(result.Data);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenNameIsEmpty()
        {
            // Arrange
            var registerUserDto = new RegisterUserDto
            {
                Name = "",
                Email = "test@example.com",
                Password = "Password123!"
            };

            // Act
            var result = await _userService.RegisterUserAsync(registerUserDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorCodes.InvalidNameFormat, result.Error);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenEmailIsInvalid()
        {
            // Arrange
            var registerUserDto = new RegisterUserDto
            {
                Name = "Test User",
                Email = "invalid-email",
                Password = "Password123!"
            };

            // Act
            var result = await _userService.RegisterUserAsync(registerUserDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorCodes.InvalidEmailFormat, result.Error);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnFailure_WhenPasswordIsWeak()
        {
            // Arrange
            var registerUserDto = new RegisterUserDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "123"
            };

            // Act
            var result = await _userService.RegisterUserAsync(registerUserDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorCodes.InvalidPasswordFormat, result.Error);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto
            {
                Email = "test@example.com",
                Password = "ValidPassword123!",
                TokenSecret = "SomeReallyReallyReallyReallyLongTokenSecret"
            };

            var user = new User
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(loginRequestDto.Password)
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.AuthenticateUser(loginRequestDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.IsType<LoginResponseDto>(result.Data);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnFailure_WhenEmailIsInvalid()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto
            {
                Email = "invalid@example.com",
                Password = "ValidPassword123!"
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(default(User));

            // Act
            var result = await _userService.AuthenticateUser(loginRequestDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorCodes.InvalidCredentials, result.Error);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnFailure_WhenPasswordIsIncorrect()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto
            {
                Email = "test@example.com",
                Password = "WrongPassword123!"
            };

            var user = new User
            {
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("hashedpassword")
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.AuthenticateUser(loginRequestDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorCodes.InvalidCredentials, result.Error);
        }
    }
}
