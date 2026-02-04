using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using ReviewAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReviewAPI.Services
{
    public class JwtService : IJwtService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;

        public JwtService(IConfiguration configuration, UserManager<Users> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessToken(Users user)
        {
            var identifier = string.IsNullOrWhiteSpace(user.Email) ? user.UserName : user.Email;

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", user.UserName ?? ""),
                new Claim("displayname", user.DisplayName ?? "")
            };

            var roles = await _userManager.GetRolesAsync(user);
            authClaims.AddRange(
                roles.Select(role => new Claim("role", role))
            );

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)
                );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string GenerateRefreshToken()
        {
            int length = 32;
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var token = new char[length];
            
            if(Environment.Version.Major >= 8)
            {
                return RandomNumberGenerator.GetString(allowedChars, length);
            }

            for (int i = 0; i < length; i++)
            {
                token[i] = allowedChars[RandomNumberGenerator.GetInt32(0, allowedChars.Length)];
            }

            return new string(token);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredTokens(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)
                ),
                ValidateLifetime = false,
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidAudience = _configuration["JWT:ValidAudience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            
            if (securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }
    }
}
