using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.JwtToken;
using GetMyTicket.Service.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ConnectionMultiplexer muxer;
        private readonly IDatabase RedisDb;

        private readonly TokenService TokenService;

        public AuthenticationController(
            TokenService tokenService)
        {
            TokenService = tokenService;

            muxer = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { { "redis-12612.c282.east-us-mz.azure.redns.redis-cloud.com", 12612 } },
                User = "default",
                Password = "s41T7yKp9FBniXGMm40iArwQAB2YqlOv"
            }
            );

            RedisDb = muxer.GetDatabase();
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken, Guid userId)
        {
            var tokenExists = await RedisDb.KeyExistsAsync(refreshToken);

            if (!tokenExists)
            {
                return BadRequest("Something went wrong. Please proceed to log in");
            }

            var tokenModel = GenerateTokens(userId);

            await RedisDb.KeyRenameAsync(refreshToken, tokenModel.RefreshToken);

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

            //TODO - encrypt token and store encrypted value
            //TODO - > CAN WE OPTIMIZE THIS? Its kinda slow
            await RedisDb.SetAddAsync(tokenModel.RefreshToken, data.Email);

            return Ok(tokenModel);

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
          var result = await RedisDb.KeyDeleteAsync(refreshToken);
            
            if (!result)
            {
                return BadRequest("Something went wrong.");
            }

            return Ok("Successfully logged out.");
        }


        private JwtTokenModel GenerateTokens(Guid userId)
        {
            string accessToken = TokenService.GenerateAccessToken(userId);
            string refreshToken = TokenService.GenerateRefreshToken();

            var tokenModel = new JwtTokenModel(accessToken, refreshToken);

            return tokenModel;

        }

    }
}
