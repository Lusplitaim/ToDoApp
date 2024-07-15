using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Data.Repositories
{
    public interface ITodoRepository
    {
        TodoEntity? Get(int todoId);
        Task<List<TodoEntity>> GetByUserIdAsync(int userId, TodoFilters filters);
        TodoEntity Create(TodoEntity entity);
        TodoEntity Update(TodoEntity entity);
        TodoEntity Delete(TodoEntity entity);
    }
}
