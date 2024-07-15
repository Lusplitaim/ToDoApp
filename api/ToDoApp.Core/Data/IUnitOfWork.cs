using Microsoft.EntityFrameworkCore.Storage;
using ToDoApp.Core.Data.Repositories;

namespace ToDoApp.Core.Data
{
    public interface IUnitOfWork
    {
        ITodoRepository TodoRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
        IDbContextTransaction BeginTransaction();
    }
}
