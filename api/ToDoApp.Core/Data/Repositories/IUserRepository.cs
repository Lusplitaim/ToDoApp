using ToDoApp.Core.Data.Entities;

namespace ToDoApp.Core.Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAsync();
        UserEntity? Get(int userId);
    }
}
