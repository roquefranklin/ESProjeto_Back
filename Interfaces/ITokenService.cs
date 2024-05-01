using ESProjeto_Back.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ESProjeto_Back.Interfaces
{
    public interface ITokenService
    {
        public Task StoreToken(Token token);
        Token? FindByToken(string refreshToken);
        public static string GenerateRefreshToken(string? userEmail, string secret, string audience, string issuer)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            var jti = Guid.NewGuid().ToString();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, userEmail),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
            };

            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(30),
                TokenType = "rt+jwt"
            });
            //await UpdateLastGeneratedClaim(userManager, userEmail, jti);
            var encodedJwt = handler.WriteToken(securityToken);
            return encodedJwt;
        }

        static string GenerateAccessToken(string userId,
                                          string userEmail,
                                          string secret,
                                          string audience,
                                          string issuer)
        {
            var key = Encoding.ASCII.GetBytes(secret);

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaim(new Claim(ClaimTypes.Email, userEmail));

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userId));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, userEmail));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = identityClaims,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(60),
                IssuedAt = DateTime.UtcNow,
                TokenType = "at+jwt"
            });

            var encodedJwt = handler.WriteToken(securityToken);
            return encodedJwt;
        }

        static async Task UpdateLastGeneratedClaim(UserManager<IdentityUser> userManager, string use, string? email, string jti)
        {
            var user = await userManager.FindByEmailAsync(email);
            var claims = await userManager.GetClaimsAsync(user);
            var newLastRtClaim = new Claim("LastRefreshToken", jti);

            var claimLastRt = claims.FirstOrDefault(f => f.Type == "LastRefreshToken");
            if (claimLastRt != null)
                await userManager.ReplaceClaimAsync(user, claimLastRt, newLastRtClaim);
            else
                await userManager.AddClaimAsync(user, newLastRtClaim);

        }

    }
}
