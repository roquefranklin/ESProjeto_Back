using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ESProjeto_Back.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TokenController : ControllerBase
    {
        private static ITokenService _tokenService;
        private static Config _config;
        private static IUserService _userService;
        public TokenController(ITokenService tokenService, IConfiguration config, IUserService userService)
        {
            _tokenService = tokenService;
            _config = config.Get<Config>()!;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] UserLogin userLogin)
        {
            try
            {

                if(userLogin.Email.IsNullOrEmpty() ||  userLogin.Password.IsNullOrEmpty())
                {
                    return BadRequest("Senha ou login em Branco");
                }

                User? user = _userService.getUserByEmail(userLogin.Email);
                if (user == null)
                {
                    return BadRequest("Senha ou login inválidos");
                }
                else if (user.Password != userLogin.Password)
                {
                    return BadRequest("Senha ou login inválidos");
                }

                string userId = Guid.NewGuid().ToString();
                string key = _config.Jwt.Key;
                string audience = _config.Jwt.Audience;
                string issuer = _config.Jwt.Issuer;

                string accessToken = ITokenService.GenerateAccessToken(
                        userId: userId,
                        userEmail: userLogin.Email,
                        secret: key,
                        audience: audience,
                        issuer: issuer
                    );

                string refreshToken = ITokenService.GenerateRefreshToken(
                        userEmail: userLogin.Email,
                        secret: key,
                        audience: audience,
                        issuer: issuer
                    );

                _tokenService.StoreToken(new Token { 
                    Id = Guid.NewGuid(),
                    IsValid = true, 
                    token = refreshToken,
                    User = user,
                    UserId = user.Id,
                    Validate = DateTime.Now,
                    TipoToken = TipoDeToken.LoginPadrao,
                });

                return Ok(new UserLoginResponse(
                    accessToken: accessToken,
                    refreshToken: refreshToken
                ));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken( [FromForm] RenewTokenDto token )
        {

            var handler = new JsonWebTokenHandler();
            string audience = _config.Jwt.Audience;
            string issuer = _config.Jwt.Issuer;

            var result = await handler.ValidateTokenAsync(token.RefreshToken, new TokenValidationParameters()
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                RequireSignedTokens = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.Jwt.Key))
            });

            if (!result.IsValid)
                return BadRequest("Expired token");

            string email = result.Claims[JwtRegisteredClaimNames.Email].ToString()!;
            string userId = result.Claims[JwtRegisteredClaimNames.Email].ToString()!;
            string key = _config.Jwt.Key;

            User user = _userService.getUserByEmail(email)!;

            var at = ITokenService.GenerateAccessToken(user.Id.ToString(), user.Email!, key, audience, issuer);
            var rt = ITokenService.GenerateRefreshToken(user.Email, key, audience, issuer);

            Token? previousToken = _tokenService.FindByToken(token.RefreshToken!);

            await _tokenService.StoreToken(new Token
            {
                Id = Guid.NewGuid(),
                IsValid = true,
                token = rt,
                User = user,
                UserId = user.Id,
                Validate = DateTime.Now,
                TipoToken = TipoDeToken.LoginPadrao,
                RefreshToken = previousToken,
            });

            return Ok(new UserLoginResponse(at, rt));

        }
    }
}
