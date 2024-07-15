using ToDoApp.Core.Data.Entities;

namespace ToDoApp.Core.Data.Repositories
{
    public interface ITodoRepository
    {
        TodoEntity? Get(int todoId);
        Task<List<TodoEntity>> GetByUserIdAsync(int userId);
        TodoEntity Create(TodoEntity entity);
        TodoEntity Update(TodoEntity entity);
        TodoEntity Delete(TodoEntity entity);
    }
}
