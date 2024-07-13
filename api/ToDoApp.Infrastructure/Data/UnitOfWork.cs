using ToDoApp.Core.Data;

namespace ToDoApp.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        //public IToDoRepository ToDoRepository => new ToDoRepository(m_DbContext);

        public async Task SaveAsync()
        {
            await m_DbContext.SaveChangesAsync();
        }
    }
}
