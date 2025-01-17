using GetMyTicket.Common.DTOs;
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
            bool parseResult = DateOnly.TryParse(registerUserDTO.DOB, out DateOnly result);

            if (!parseResult)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "Invalid date of birth",
                    Description = "The type of the provided DOB is incorrect."
                });
            }

            var user = new User()
            {
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Email = registerUserDTO.Email,
                UserName = registerUserDTO.Username,
                IsSubscribedForNewsletter = registerUserDTO.IsSubscribedForNewsletter,
                DOB = result,
                Address = registerUserDTO.Address
            };

            //CREATE USER
            return await userManager.CreateAsync(user, registerUserDTO.Password);

            //RETURN AN ANSWER
        }
    }
}
