using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Core.DTOs.User;
using ToDoApp.Core.Extensions;
using ToDoApp.Core.Services;

namespace ToDoApp.API.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthService m_AuthService;
        private readonly IUsersService m_UsersService;
        public AuthenticationController(IAuthService service, IUsersService usersService)
        {
            m_AuthService = service;
            m_UsersService = usersService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(LoginUserDto model)
        {
            var result = await m_AuthService.AuthenticateUserAsync(model);
            /*if (success)
            {
                var token = await m_AuthService.CreateTokenAsync(model.Email);
                var user = await m_UsersService.GetUserAsync(model.Email);
                return Ok(new { user, token });
            }*/

            return this.ResolveResult(result, () => Ok(result.Result));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDto model)
        {
            var result = await m_AuthService.RegisterUserAsync(model);
            /*if (result.Succeeded)
            {
                var token = await m_AuthService.CreateTokenAsync(model.Email);
                var user = await m_UsersService.GetUserAsync(model.Email);
                return CreatedAtAction(nameof(RegisterUser), new { user, token });
            }*/

            return this.ResolveResult(result, () => CreatedAtAction(nameof(RegisterUser), result.Result));
        }
    }
}
