using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using GetMyTicket.Service.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using GetMyTicket.Common.DTOs.User;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly JwtTokenService jwtTokenService;

        [HttpPost("login")]
        public IActionResult Login(LoginDTO data)
        {
           
                var token = jwtTokenService.GenerateAccessToken(data.Email);
                return Ok(new { Token = token });
            
        }

        //TODO IMPLEMENT LOGOUT 
    }
}
