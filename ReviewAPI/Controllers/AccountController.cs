using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReviewAPI.DTOs;
using ReviewAPI.Models;
using ReviewAPI.Services;
using ReviewAPI.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReviewAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IAccountEmailService _accountEmailService;
        private readonly AppSettings _appSettings;
        private readonly IJwtService _jwtService;

        public AccountController(
            UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            IAccountEmailService accountEmailService,
            IOptions<AppSettings> appSettings,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountEmailService = accountEmailService;
            _appSettings = appSettings.Value;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {

            var user = new Users
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, Roles.User);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var confirmationLink = $"{_appSettings.FrontendUrl}/verify-email/{user.Id}/{encodedToken}";
            Console.Write(confirmationLink);

            await _accountEmailService.SendVerificationEmailAsync(
                user.Email!,
                confirmationLink
                );

            return Ok(new { message = "Account Created. A verification email has been sent to your email address." });
        }

        [AllowAnonymous]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) 
                return BadRequest("Invalid Request");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) 
                return BadRequest("User not Found");

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (result.Succeeded)
                return Ok("Email Verified");

            return BadRequest("Invalid Or Expired Token");

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            var user = (new EmailAddressAttribute().IsValid(dto.Identifier)) ? 
                await _userManager.FindByEmailAsync(dto.Identifier) : await _userManager.FindByNameAsync(dto.Identifier);

            if (user == null)
            {
                return Unauthorized("Invalid Login Credentials");
            }

            if (!user.EmailConfirmed)
            {
                return Unauthorized("Email not Verified, Please check your Email's Inbox and Spam");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                dto.Password,
                lockoutOnFailure: false
                );

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid Login Credentials");
            }

            var accessToken = await _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            SetAuthCookies(accessToken, refreshToken);

            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new UserDto
            {
                id = user.Id,
                displayName = user.DisplayName,
                roles = roles,
                userName = user.UserName!,
                email = user.Email!
            };

            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refresh_token"];

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                Console.WriteLine("Cookies missing!");
                return BadRequest("Tokens Not Found");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(user =>
                user.RefreshToken == refreshToken && user.RefreshTokenExpiryTime > DateTime.UtcNow);

            if (user == null)
            {
                return BadRequest("Invalid Refresh Token");
            }

            var newAccessToken = await _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            SetAuthCookies(newAccessToken, newRefreshToken);

            return Ok();

        }

        private void SetAuthCookies(string accessToken, string refreshToken)
        {
            Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append(
                "access_token",
                string.Empty,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(-1)
                }
            );

            Response.Cookies.Append(
                "refresh_token",
                string.Empty,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(-1)
                }
            );

            return Ok();

        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAccount()
        {
            Console.WriteLine("! GetAccount Ran !");
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId is null) 
            {
                Console.WriteLine(userId);
                return NotFound(); 
            }

            var user = await _userManager.FindByIdAsync(userId!);
            if (user is null) 
            {
                Console.WriteLine("! User not Found !");
                Console.WriteLine(userId);
                return NotFound(); 
            }

            AccountDto returnDto = new AccountDto();

            returnDto.DisplayName = user.DisplayName;
            returnDto.Description = user.Description;
            returnDto.Birthday = user.Birthday;
            returnDto.Pronouns = user.Pronouns;
            returnDto.SafeMode = user.SafeMode;

            return Ok(returnDto);
        }

        [AllowAnonymous]
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountDto accountInfo)
        {
            var user = await _userManager.FindByIdAsync(JwtRegisteredClaimNames.Sub);
            if (user is null) return NotFound();

            if (accountInfo.DisplayName is not null) user.DisplayName = accountInfo.DisplayName!;
            if (accountInfo.Description is not null) user.Description = accountInfo.Description!;
            if (accountInfo.Birthday is not null) user.Birthday = accountInfo.Birthday!;
            if (accountInfo.Pronouns is not null) user.Pronouns = accountInfo.Pronouns!;
            if (accountInfo.SafeMode != user.SafeMode) user.SafeMode = accountInfo.SafeMode;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }

    }
}
