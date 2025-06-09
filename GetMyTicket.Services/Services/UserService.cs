using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.ErrorHandling;
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

        public async Task<IdentityResult> CreateUserAsync(CreateUserDTO registerUserDTO)
        {
            //TODO: decide on a way to bypass username validation OR Decide if a username is actually needed

            if (string.IsNullOrWhiteSpace(registerUserDTO.Email))
            {
                throw new ApplicationError(ResponseConstants.AllFieldsRequired);
            }

            var user = new User()
            {
                Email = registerUserDTO.Email.Trim(),
                IsSubscribedForNewsletter = registerUserDTO.NewsletterSubscribtion,
                UserName = registerUserDTO.Email.Trim()
            };

             return await userManager.CreateAsync(user, registerUserDTO.Password);
        }
    }
}
