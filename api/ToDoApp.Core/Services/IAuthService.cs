using ToDoApp.Core.DTOs.User;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Services
{
    public interface IAuthService
    {
        Task<ExecResult<AuthResult>> RegisterUserAsync(RegisterUserDto model);
        Task<ExecResult<AuthResult>> AuthenticateUserAsync(LoginUserDto model);
    }
}