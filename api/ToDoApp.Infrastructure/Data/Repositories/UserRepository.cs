using Microsoft.EntityFrameworkCore;
using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.Data.Repositories;

namespace ToDoApp.Infrastructure.Data.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly DatabaseContext m_DbContext;
        public UserRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<List<UserEntity>> GetAsync()
        {
            return await m_DbContext.Users.ToListAsync();
        }
    }
}
