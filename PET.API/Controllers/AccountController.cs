using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PET.Application.DTOs;
using PET.Application.Services;

namespace PET.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserAppService userAppService;

        public AccountController(UserAppService userAppService)
        {
            this.userAppService = userAppService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Login()
        {
            HttpContext.Response.StatusCode = 403;
            return new JsonResult(new { Error = "Ошибка авторизации." });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserSaveDto userDto)
        {
            try
            {
                var user = await userAppService.Get(userDto.Email);

                if (user != null && user.Password == userDto.Password)
                {
                    await Authenticate(userDto.Email);

                    return new OkResult();
                }
            }
            catch
            {

            }

            return Redirect("/account");
        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return new OkResult();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}