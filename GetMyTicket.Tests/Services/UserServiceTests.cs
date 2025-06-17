using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.ErrorHandling;
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

  ////   [Fact]
  //   public async Task RegisterUserAsync_ValidDTO_CreatesUserSuccessfully()
  //   {
  //       // Arrange
  //       var dto = new CreateUserDTO(
  //           "Tourist",
  //            "Guy",
  //           "touristguy11@gmail.com",
  //           "Passw1rd#",
  //           "1997-01-01",
  //           true,
  //           "Some address 15");
  //
  //       userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
  //           .ReturnsAsync(IdentityResult.Success);
  //
  //       // Act
  //       var result = await userService.CreateUserAsync(dto);
  //
  //       // Assert
  //       Assert.True(result.Succeeded);
  //   }
  //
  //   [Fact]
  //   public async Task RegisterUserAsync_InvalidDob_ThrowsError()
  //   {
  //       var registerUserDTO = new CreateUserDTO {
  //           Email = "touristguy11@gmail.com",
  //           Password = "Passw1rd#",
  //           NewsletterSubscribtion = true 
  //       };
  //
  //       await Assert.ThrowsAsync<ApplicationError>(() => userService.CreateUserAsync(registerUserDTO));
  //   }
  //
     //  [Theory]
     //  [InlineData("", "Last", "email@example.com", "Address")]
     //  [InlineData("First", "", "email@example.com", "Address")]
     //  [InlineData("First", "Last", "", "Address")]
     //  [InlineData("First", "Last", "email@example.com", "")]
     //
     //  //test empty first name/ last name/ email/ address
     //  public async Task RegisterUserAsync_MissingRequiredFields_ThrowsException(
     // string firstName, string lastName, string email, string address)
     //  {
     //      var dto = new CreateUserDTO{
     //      Email = email, 
     //      Password = pas};
     //
     //      await Assert.ThrowsAsync<ApplicationError>(() => userService.CreateUserAsync(dto));
     //  }
     //
     //  [Fact]
     //  public async Task RegisterUserAsync_CreationFails_ReturnsIdentityError()
    //  {
    //      // Arrange
    //      var dto = new CreateUserDTO(
    //          "Tourist",
    //           "Guy",
    //          "touristguy11@gmail.com",
    //          "lets say my passwords not ok",
    //          "1997-01-01",
    //          true,
    //          "Some address 15");
    //
    //      userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
    //          .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to create user" }));
    //
    //      // Act
    //      var result = await userService.CreateUserAsync(dto);
    //
    //      // Assert
    //      Assert.False(result.Succeeded);
    //    }
    }

}
