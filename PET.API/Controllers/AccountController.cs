using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PET.Application.Builders;
using PET.Application.DTOs;
using PET.Application.Services;

namespace PET.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserAppService userAppService;

        public AccountController(UserAppService userAppService)
        {
            this.userAppService = userAppService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Login()
        {
            HttpContext.Response.StatusCode = 403;

            return new JsonResult(new {Error = "Ошибка авторизации."});
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var user = await userAppService.Get(userDto.Email);

            if (user != null && user.Password == userDto.Password)
            {
                await Authenticate(userDto.Email);

                return new OkResult();
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

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var user = await userAppService.Get(userRegisterDto.Email);

            if (user != null)
            {
                return UnprocessableEntity(new {Error = "Пользователь с таким email уже существует."});
            }

            await userAppService.Create(userRegisterDto);

            return new OkResult();
        }
    }
}