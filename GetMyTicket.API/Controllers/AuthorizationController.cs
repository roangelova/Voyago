using System.Security.Claims;
using System.Text.Json;
using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.JwtToken;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace GetMyTicket.API.Controllers
{
    [Route("api/authorization")]
    [ApiController]

    public class AuthorizationController : ControllerBase
    {
        //TODO: REFACTOR!

        private readonly ConnectionMultiplexer muxer;
        private readonly IDatabase RedisDb;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        private readonly IAuthorizationService authorizationService;

        public AuthorizationController(
            IAuthorizationService authorizationService,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            this.authorizationService = authorizationService;
            this.userManager = userManager;

            var redisConfig = configuration.GetSection("Redis");

            muxer = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { { redisConfig["EndPoints:0:Host"], int.Parse(redisConfig["EndPoints:0:Port"]) } },
                User = redisConfig["User"],
                Password = redisConfig["Password"]
            }
            );

            RedisDb = muxer.GetDatabase();
            this.configuration = configuration;
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken, Guid userId)
        {
            var tokenExists = await RedisDb.KeyExistsAsync(refreshToken);

            if (!tokenExists)
            {
                return BadRequest(ResponseConstants.SomethingWentWrong);
            }

            var tokenModel = GenerateTokens(userId);

            await RedisDb.KeyRenameAsync(refreshToken, tokenModel.RefreshToken);

            return Ok(tokenModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO data)
        {
            var user = await userManager.FindByEmailAsync(data.Email);

            if (user == null)
            {
                return BadRequest(ResponseConstants.InvalidCredentials);
            }

            bool passwordIsCorrect = await userManager.CheckPasswordAsync(
                user, data.Password);

            if (!passwordIsCorrect)
            {
                return BadRequest(ResponseConstants.InvalidCredentials);
            }

            var tokenModel = GenerateTokens(user.Id);

            var authorizedUser = new AuthorizedUserCacheModel()
            {
                AccessToken = tokenModel.AccessToken,
                RefreshToken = tokenModel.RefreshToken
            };

            //Save Tokens by userId as key in Redis Cache
            //TODO -> USE LOCAL CACHE IN DEVELOPMENT?
            await RedisDb.SetAddAsync(user.Id.ToString(), JsonSerializer.Serialize(authorizedUser));

            return Ok(tokenModel);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId is null) return BadRequest(ResponseConstants.SomethingWentWrong);

            var result = await RedisDb.KeyDeleteAsync(userId);

            if (!result) return BadRequest(ResponseConstants.SomethingWentWrong);

            return Ok(ResponseConstants.LogoutSuccessful);
        }


        private JwtTokenModel GenerateTokens(Guid userId)
        {
            string accessToken = authorizationService.GenerateAccessToken(userId);
            string refreshToken = authorizationService.GenerateRefreshToken();

            var tokenModel = new JwtTokenModel(accessToken, refreshToken, userId);
            return tokenModel;
        }

    }
}