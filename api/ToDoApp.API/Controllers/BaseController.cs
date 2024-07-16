using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Core.Filters;

namespace ToDoApp.API.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter<RestExceptionFilter>]
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
