using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.JwtToken;
using GetMyTicket.Service.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IDistributedCache RedisInstance;

        private readonly TokenService TokenService;

        public AuthenticationController(
            IDistributedCache redisInstance,
            TokenService tokenService)
        {
            RedisInstance = redisInstance;
            TokenService = tokenService;
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken (string refreshToken, Guid userId)
        {
            if(await RedisInstance.GetAsync(refreshToken) == null)
            {
                return BadRequest("Something went wrong. Please proceed to log in");
            }

            var tokenModel = GenerateTokens(userId);

            return Ok(tokenModel);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO data)
        {
            //TODO -> validate login data
            //ignore for now for test purposes

            //TODO -> get userId
            Guid userId = Guid.CreateVersion7();

           var tokenModel = GenerateTokens(userId);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes
                    (JwtTokenModel._RefreshTokenTokenExpiration) 
            };

            //TODO - encrypt token and store encrypted value
            //TODO - > fix connection bug - PROVIDE CREDENTIALS
            await RedisInstance.SetStringAsync(tokenModel.RefreshToken, data.Email );

            return Ok(tokenModel);

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            await RedisInstance.RemoveAsync(refreshToken);

            return Ok("Successfully logged out.");
        }


        private JwtTokenModel GenerateTokens(Guid userId) {

            string accessToken = TokenService.GenerateAccessToken(userId);
            string refreshToken = TokenService.GenerateRefreshToken();

            var tokenModel = new JwtTokenModel(accessToken, refreshToken);

            return tokenModel;

        }

    }
}
