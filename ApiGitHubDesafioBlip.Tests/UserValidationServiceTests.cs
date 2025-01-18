using ApiGithubDesafioBlip.Application.Interfaces;
using Moq;

namespace ApiGitHubDesafioBlip.Tests;

public class UserValidationServiceTests
{
    private readonly Mock<IUserValidationService> _mockService;

    public UserValidationServiceTests()
    {
        _mockService = new Mock<IUserValidationService>();
        _mockService.Setup(s => s.UserExists(It.IsAny<string>())).ReturnsAsync(true);
    }

    [Fact]
    public async Task UserExists_ValidUser_ShouldReturnTrue()
    {
        // Arrange
        var service = _mockService.Object;

        // Act
        var result = await service.UserExists("jpemendonca");
        
        // Assert
        Assert.True(result);
    }
}