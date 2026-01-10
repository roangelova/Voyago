using System.Security.Claims;
using System.Text.Json;
using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.JwtToken;
using GetMyTicket.Service.Caching;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/authorization")]
    [ApiController]

    public class AuthorizationController : ControllerBase
    {
        private readonly ICachingService cachingService;
        private readonly UserManager<User> userManager;
        private readonly IAuthorizationService authorizationService;

        public AuthorizationController(
            IAuthorizationService authorizationService,
            UserManager<User> userManager,
            ICachingService cachingService
            )
        {
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.cachingService = cachingService;
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            var userData = await cachingService.Get<AuthorizedUserCacheModel>(refreshTokenDTO.UserId.ToString());

            //User is not logged in
            if (userData == null)
            {
                return BadRequest(ResponseConstants.SomethingWentWrong);
            }

            if (userData.RefreshToken != refreshTokenDTO.RefreshToken)
            {
                return Unauthorized();
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
                return Unauthorized(ResponseConstants.InvalidCredentials);
            }

            bool passwordIsCorrect = await userManager.CheckPasswordAsync(
                user, data.Password);

            if (!passwordIsCorrect)
            {
                return Unauthorized(ResponseConstants.InvalidCredentials);
            }

            var userIsLoggedIn = await cachingService.Get<AuthorizedUserCacheModel>(user.Id.ToString());

            if (userIsLoggedIn != null)
            {
                await cachingService.Remove(user.Id.ToString());
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

            await cachingService.Remove(userId);

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

            await cachingService.Set(
                userId.ToString(),
                JsonSerializer.Serialize(authorizedUser),
                TimeSpan.FromMinutes(JwtTokenModel._RefreshTokenTokenExpiration));
           
            return tokenModel;
        }
    }
}