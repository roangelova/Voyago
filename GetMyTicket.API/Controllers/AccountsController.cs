using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using static GetMyTicket.Common.Constants.ResponseConstants;

namespace GetMyTicket.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountsController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO registerUserDTO)
        {
            var result = await userService.CreateUserAsync(registerUserDTO);


            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                throw new ApplicationError(string.Join(" ", result.Errors.Select(x => x.Description)));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> CloseAccount(string userId)
        {
            await userService.CloseAccount(userId);

            return NoContent();
        }
    }
}
