using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using ReviewAPI.Models;
using System.Security.Claims;

namespace ReviewAPI.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAccessToken(Users user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredTokens(string token);
    }
}
