using ToDoApp.Core.Data;
using ToDoApp.Core.DTOs.Todo;
using ToDoApp.Core.Exceptions;
using ToDoApp.Core.Models;
using ToDoApp.Core.Storages;
using ToDoApp.Core.Utils;

namespace ToDoApp.Core.Services
{
    internal class TodosService : ITodosService
    {
        private readonly ITodoStorage m_TodoStorage;
        private readonly IAuthUtils m_AuthUtils;
        private readonly IUnitOfWork m_UnitOfWork;
        public TodosService(ITodoStorage todoStorage, IAuthUtils authUtils, IUnitOfWork uow)
        {
            m_TodoStorage = todoStorage;
            m_AuthUtils = authUtils;
            m_UnitOfWork = uow;
        }

        public async Task<IEnumerable<TodoDto>> GetAsync(TodoFilters filters)
        {
            try
            {
                var currentUserId = m_AuthUtils.GetAuthUserId();

                var result = await m_TodoStorage.GetByUserIdAsync(currentUserId, filters);

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to get todos", ex);
            }
        }

        public async Task<ExecResult<TodoDto>> CreateAsync(CreateTodoDto model)
        {
            try
            {
                using var transaction = m_UnitOfWork.BeginTransaction();

                var currentUserId = m_AuthUtils.GetAuthUserId();
                var result = await m_TodoStorage.CreateAsync(currentUserId, model);

                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to create todo", ex);
            }
        }

        public async Task<ExecResult<TodoDto>> UpdateAsync(int todoId, UpdateTodoDto model)
        {
            try
            {
                var todo = await m_TodoStorage.GetAsync(todoId);
                var result = await m_TodoStorage.UpdateAsync(todoId, model);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to update todo", ex);
            }
        }

        public async Task<ExecResult> DeleteAsync(int todoId)
        {
            try
            {
                var todo = await m_TodoStorage.GetAsync(todoId);
                var result = await m_TodoStorage.DeleteAsync(todoId);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to delete todo", ex);
            }
        }
    }
}
