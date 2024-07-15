﻿using ToDoApp.Core.DTOs.Todo;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Services
{
    public interface ITodosService
    {
        Task<IEnumerable<TodoDto>> GetAsync();
        Task<ExecResult<TodoDto>> CreateAsync(CreateTodoDto model);
        Task<ExecResult<TodoDto>> UpdateAsync(int todoId, UpdateTodoDto model);
        Task<ExecResult> DeleteAsync(int todoId);
    }
}