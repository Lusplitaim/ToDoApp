using ToDoApp.Core.DTOs.User;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Storages
{
    public interface IUserStorage
    {
        Task<ExecResult> CreateAsync(RegisterUserDto model);
        Task<UserDto> GetAsync(string email);
    }
}
