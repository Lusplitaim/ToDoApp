using ToDoApp.Core.Data;
using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.DTOs.Todo;
using ToDoApp.Core.Exceptions;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Storages
{
    internal class TodoStorage : ITodoStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        public TodoStorage(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;
        }

        public Task<TodoDto> GetAsync(int todoId)
        {
            var entity = m_UnitOfWork.TodoRepository.Get(todoId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            return Task.FromResult(TodoDto.From(entity));
        }

        public async Task<List<TodoDto>> GetByUserIdAsync(int userId)
        {
            var entities = await m_UnitOfWork.TodoRepository.GetByUserIdAsync(userId);
            return entities.Select(TodoDto.From).ToList();
        }

        public async Task<ExecResult<TodoDto>> CreateAsync(int creatorId, CreateTodoDto model)
        {
            var result = new ExecResult<TodoDto>();
            TodoEntity entity = new()
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                PriorityLevel = model.Priority,
                CreatorId = creatorId,
            };

            var createdEntity = m_UnitOfWork.TodoRepository.Create(entity);
            await m_UnitOfWork.SaveAsync();
            result.Result = TodoDto.From(createdEntity);

            return result;
        }

        public async Task<ExecResult<TodoDto>> UpdateAsync(int todoId, UpdateTodoDto model)
        {
            var result = new ExecResult<TodoDto>();

            var entity = m_UnitOfWork.TodoRepository.Get(todoId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.DueDate = model.DueDate;
            entity.PriorityLevel = model.Priority;

            m_UnitOfWork.TodoRepository.Update(entity);
            await m_UnitOfWork.SaveAsync();
            result.Result = TodoDto.From(entity);

            return result;
        }

        public async Task<ExecResult> DeleteAsync(int todoId)
        {
            var result = new ExecResult();

            var entity = m_UnitOfWork.TodoRepository.Get(todoId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            m_UnitOfWork.TodoRepository.Delete(entity);
            await m_UnitOfWork.SaveAsync();

            return result;
        }
    }
}
