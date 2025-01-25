using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using GetMyTicket.Service.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.JwtToken;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly TokenService TokenService;

        [HttpPost("login")]
        public IActionResult Login(LoginDTO data)
        {
            //TODO -> validate login data
            //ignore for now for test purposes

            string accessToken = TokenService.GenerateAccessToken(data.Email);
            string refreshToken = TokenService.GenerateRefreshToken();

            var token = new JwtTokenModel(accessToken, refreshToken);

            return Ok(token);

        }

        //TODO IMPLEMENT LOGOUT 
    }
}
