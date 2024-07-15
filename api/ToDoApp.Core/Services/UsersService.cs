using ToDoApp.Core.DTOs.User;
using ToDoApp.Core.Exceptions;
using ToDoApp.Core.Storages;

namespace ToDoApp.Core.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IUserStorage m_UserStorage;
        public UsersService(IUserStorage userStorage)
        {
            m_UserStorage = userStorage;
        }

        public async Task<UserDto> GetUserAsync(string userEmail)
        {
            try
            {
                var result = await m_UserStorage.GetAsync(userEmail);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to get user", ex);
            }
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            try
            {
                var result = await m_UserStorage.GetAsync();
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to get users", ex);
            }
        }
    }
}
