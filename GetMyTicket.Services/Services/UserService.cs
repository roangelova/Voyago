using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.Entities;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Identity;

namespace GetMyTicket.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            //check and convert DOB
            bool parseResult = DateOnly.TryParse(registerUserDTO.Dob, out DateOnly result);

            if (!parseResult)
            {
                throw new InvalidDataException("Invalid date of birth");
            }

            //TODO: write unittest to check this method 

            //TODO: decide on a way to bypass username validation OR Decide if a username is actually needed

            if (string.IsNullOrWhiteSpace(registerUserDTO.FirstName) ||
                   string.IsNullOrWhiteSpace(registerUserDTO.LastName) ||
                   string.IsNullOrWhiteSpace(registerUserDTO.Email) ||
                   string.IsNullOrWhiteSpace(registerUserDTO.Address))
            {
                throw new ArgumentException("A required field was empty.");
            }

            var user = new User()
            {
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Email = registerUserDTO.Email,
                IsSubscribedForNewsletter = registerUserDTO.NewsletterSubscribtion,
                DOB = result,
                UserName = registerUserDTO.Email,
                Address = registerUserDTO.Address,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            //CREATE USER
            return await userManager.CreateAsync(user, registerUserDTO.Password);
        }
    }
}
