using Microsoft.EntityFrameworkCore.Storage;
using ToDoApp.Core.Data;
using ToDoApp.Core.Data.Repositories;
using ToDoApp.Infrastructure.Data.Repositories;

namespace ToDoApp.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public ITodoRepository TodoRepository => new TodoRepository(m_DbContext);

        public async Task SaveAsync()
        {
            await m_DbContext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return m_DbContext.Database.BeginTransaction();
        }
    }
}
