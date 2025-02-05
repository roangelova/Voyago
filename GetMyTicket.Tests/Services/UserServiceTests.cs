using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.Entities;
using GetMyTicket.Service.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace GetMyTicket.Tests.Services
{

    public class UserServiceTests
    {
        private readonly Mock<UserManager<User>> userManagerMock;
        private readonly UserService userService;

        public UserServiceTests()
        {
            userManagerMock = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);

            userService = new UserService(userManagerMock.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_ValidDTO_CreatesUserSuccessfully()
        {
            // Arrange
            var dto = new RegisterUserDTO(
                "Tourist",
                 "Guy",
                "touristguy11@gmail.com",
                "Passw1rd#",
                "1997-01-01",
                true,
                "Some address 15");

            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await userService.RegisterUserAsync(dto);

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task RegisterUserAsync_InvalidDob_ThrowsError()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO(
                "Tourist",
                 "Guy",
                "touristguy11@gmail.com",
                "Passw1rd#",
                "this is not a date",
                true,
                "Some address 15");

            await Assert.ThrowsAsync<InvalidDataException>(() => userService.RegisterUserAsync(registerUserDTO));
        }

        [Fact]
        public async Task RegisterUserAsync_EmptyRequiredFields_ThrowsException()
        {
            // Arrange
            var dto = new RegisterUserDTO(
                "",
                 "Guy with no first name",
                "touristguy11@gmail.com",
                "Passw1rd#",
                "1997-01-01",
                true,
                "Some address 15");

            await Assert.ThrowsAsync<ArgumentException>(() => userService.RegisterUserAsync(dto));
        }

        [Fact]
        public async Task RegisterUserAsync_CreationFails_ReturnsIdentityError()
        {
            // Arrange
            var dto = new RegisterUserDTO(
                "Tourist",
                 "Guy",
                "touristguy11@gmail.com",
                "lets say my passwords not ok",
                "1997-01-01",
                true,
                "Some address 15");

            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to create user" }));

            // Act
            var result = await userService.RegisterUserAsync(dto);

            // Assert
            Assert.False(result.Succeeded);
        }
    }

}
