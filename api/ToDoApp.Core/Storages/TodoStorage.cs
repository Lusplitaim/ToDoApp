using FluentValidation;
using ToDoApp.Core.Data;
using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.DTOs.Todo;
using ToDoApp.Core.Exceptions;
using ToDoApp.Core.Models;
using ToDoApp.Core.Utils;

namespace ToDoApp.Core.Storages
{
    internal class TodoStorage : ITodoStorage
    {
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly IValidator<ModificationModel<TodoEntity>> m_Validator;
        private readonly IAuthUtils m_AuthUtils;
        public TodoStorage(IUnitOfWork uow, IValidator<ModificationModel<TodoEntity>> validator, IAuthUtils authUtils)
        {
            m_UnitOfWork = uow;
            m_Validator = validator;
            m_AuthUtils = authUtils;
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

        public async Task<List<TodoDto>> GetByUserIdAsync(int userId, TodoFilters filters)
        {
            var entities = await m_UnitOfWork.TodoRepository.GetByUserIdAsync(userId, filters);
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
                PriorityLevel = (int)model.Priority,
                CreatorId = creatorId,
                AssignedUserId = model.AssignedUserId,
            };

            var validResult = m_Validator.Validate(new ModificationModel<TodoEntity>(null, entity));
            if (!validResult.IsValid)
            {
                result.AddErrors(validResult);
                return result;
            }

            var createdEntity = m_UnitOfWork.TodoRepository.Create(entity);
            await m_UnitOfWork.SaveAsync();
            result.Result = TodoDto.From(createdEntity);

            return result;
        }

        public async Task<ExecResult<TodoDto>> UpdateAsync(int todoId, UpdateTodoDto model)
        {
            var result = new ExecResult<TodoDto>();

            var entity = m_UnitOfWork.TodoRepository.Get(todoId, track: false);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            var currentUserId = m_AuthUtils.GetAuthUserId();
            if (currentUserId != entity.CreatorId)
            {
                throw new ForbiddenCoreException();
            }

            var forSave = (TodoEntity)entity.Clone();

            forSave.Title = model.Title;
            forSave.Description = model.Description;
            forSave.DueDate = model.DueDate;
            forSave.PriorityLevel = (int)model.Priority;
            forSave.AssignedUserId = model.AssignedUserId;

            var validResult = m_Validator.Validate(new ModificationModel<TodoEntity>(entity, forSave));
            if (!validResult.IsValid)
            {
                result.AddErrors(validResult);
                return result;
            }

            m_UnitOfWork.TodoRepository.Update(forSave);
            await m_UnitOfWork.SaveAsync();
            result.Result = TodoDto.From(entity);

            return result;
        }

        public async Task<ExecResult<TodoDto>> UpdateStatusAsync(int todoId, UpdateTodoStatusDto model)
        {
            var result = new ExecResult<TodoDto>();

            var entity = m_UnitOfWork.TodoRepository.Get(todoId);
            if (entity is null)
            {
                throw new NotFoundCoreException();
            }

            entity.IsCompleted = model.IsCompleted;

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
