using ToDoApp.Core.DTOs.Todo;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Storages
{
    public interface ITodoStorage
    {
        Task<List<TodoDto>> GetByUserIdAsync(int userId, TodoFilters filters);
        Task<TodoDto> GetAsync(int todoId);
        Task<ExecResult<TodoDto>> CreateAsync(int creatorId, CreateTodoDto model);
        Task<ExecResult<TodoDto>> UpdateAsync(int todoId, UpdateTodoDto model);
        Task<ExecResult<TodoDto>> UpdateStatusAsync(int todoId, UpdateTodoStatusDto model);
        Task<ExecResult> DeleteAsync(int todoId);
    }
}