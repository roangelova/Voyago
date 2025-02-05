using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountsController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserDTO registerUserDTO)
        {
           var result = userService.RegisterUserAsync(registerUserDTO);

            if (result.Result.Succeeded)
            {
                return Ok(result.Result);
            }

            return BadRequest(result.Result.Errors);
        }
    }
}
