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
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            var userData = await RedisDb.StringGetAsync(refreshTokenDTO.UserId.ToString());

            //User is not logged in
            if (userData.IsNull)
            {
                return BadRequest(ResponseConstants.SomethingWentWrong);
            }

            var data = JsonSerializer.Deserialize<AuthorizedUserCacheModel>(userData!);

            if (data?.RefreshToken != refreshTokenDTO.RefreshToken)
            {
                return BadRequest(ResponseConstants.SomethingWentWrong);
            }

            var tokenModel = await GenerateTokensAndSetInCache(refreshTokenDTO.UserId);

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

            bool userIsLoggedIn = await RedisDb.KeyExistsAsync(user.Id.ToString());

            if (userIsLoggedIn)
            {
                return BadRequest(ResponseConstants.SomethingWentWrong);
            }

            var tokenModel = await GenerateTokensAndSetInCache(user.Id);

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


        /// <summary>
        /// This method generates new access and refresh tokens. Then, using the userId as a key, the data is cached in a RedisD
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<JwtTokenModel> GenerateTokensAndSetInCache(Guid userId)
        {
            string accessToken = authorizationService.GenerateAccessToken(userId);
            string refreshToken = authorizationService.GenerateRefreshToken();

            var tokenModel = new JwtTokenModel(accessToken, refreshToken, userId);

            var authorizedUser = new AuthorizedUserCacheModel()
            {
                AccessToken = tokenModel.AccessToken,
                RefreshToken = tokenModel.RefreshToken
            };

            await RedisDb.StringSetAsync(
                userId.ToString(),
                JsonSerializer.Serialize(authorizedUser),
                TimeSpan.FromMinutes(JwtTokenModel._AccessTokenTokenExpiration));

            return tokenModel;
        }
    }
}