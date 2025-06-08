using System;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Roomies.Application.Enums;
using Roomies.Application.Interfaces;
using Roomies.Application.Interfaces.Repositories;
using Roomies.Application.Interfaces.Services;
using Roomies.Application.Models;
using Roomies.Application.Services;
using Roomies.Domain.Entities;
using Xunit;

namespace Roomies.Application.Tests.Services;

public class HouseholdServiceTests
{
    private readonly Mock<IHouseholdRepository> _householdRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly HouseholdService _householdService;

    public HouseholdServiceTests()
    {
        _householdRepositoryMock = new Mock<IHouseholdRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _householdService = new HouseholdService(
            _householdRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateHouseholdAsync_ShouldReturnSuccess_WhenValidRequest()
    {
        // Arrange
        var request = new CreateHouseholdRequestDto
        {
            Name = "Test Household",
            Description = "Test Description"
        };

        _currentUserServiceMock.Setup(x => x.UserId).Returns(Guid.NewGuid());
        _householdRepositoryMock
            .Setup(x => x.HasSameNameUserHouseholdAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        var result = await _householdService.CreateHouseholdAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);
        _householdRepositoryMock.Verify(x => x.AddHouseholdAsync(It.IsAny<Household>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateHouseholdAsync_ShouldReturnFailure_WhenHouseholdNameExists()
    {
        // Arrange
        var request = new CreateHouseholdRequestDto
        {
            Name = "Existing Household",
            Description = "Test Description"
        };

        _currentUserServiceMock.Setup(x => x.UserId).Returns(Guid.NewGuid());
        _householdRepositoryMock
            .Setup(x => x.HasSameNameUserHouseholdAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act
        var result = await _householdService.CreateHouseholdAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorCodes.HouseholdAlreadyExists, result.Error);
        Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        _householdRepositoryMock.Verify(x => x.AddHouseholdAsync(It.IsAny<Household>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}
