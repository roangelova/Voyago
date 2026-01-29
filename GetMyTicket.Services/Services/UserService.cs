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

            var userWithSameEmail = await userManager.FindByEmailAsync(registerUserDTO.Email);
            if (userWithSameEmail != null)
            {
                throw new ApplicationError(ResponseConstants.UserWithThisEmailExist);
            }

            var user = new User()
            {
                Email = registerUserDTO.Email.Trim(),
                IsSubscribedForNewsletter = registerUserDTO.NewsletterSubscribtion,
                UserName = registerUserDTO.Email.Trim()
            };

            return await userManager.CreateAsync(user, registerUserDTO.Password);
        }
        public async Task CloseAccount(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(User), userId));
            }

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.UtcNow;
            user.LastUpdatedAt = DateTime.UtcNow;

            await userManager.UpdateAsync(user);
        }
    }
}
