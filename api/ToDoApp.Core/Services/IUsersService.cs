using ToDoApp.Core.DTOs.User;

namespace ToDoApp.Core.Services
{
    public interface IUsersService
    {
        Task<UserDto> GetUserAsync(string userEmail);
    }
}