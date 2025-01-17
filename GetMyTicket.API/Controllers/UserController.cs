using GetMyTicket.Common.DTOs;
using GetMyTicket.Service.Contracts;
using GetMyTicket.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("registerUser")]
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
