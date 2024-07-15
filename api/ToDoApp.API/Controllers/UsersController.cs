using Microsoft.AspNetCore.Mvc;
using ToDoApp.Core.Services;

namespace ToDoApp.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService m_UsersService;
        public UsersController(IUsersService usersService)
        {
            m_UsersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await m_UsersService.GetUsersAsync();
            return Ok(result);
        }
    }
}
