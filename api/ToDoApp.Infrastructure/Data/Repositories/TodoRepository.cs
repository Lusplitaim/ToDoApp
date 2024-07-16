using Microsoft.EntityFrameworkCore;
using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.Data.Repositories;
using ToDoApp.Core.Models;

namespace ToDoApp.Infrastructure.Data.Repositories
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly DatabaseContext m_DbContext;
        public TodoRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public Task<List<TodoEntity>> GetByUserIdAsync(int userId, TodoFilters filters)
        {
            var userTodos = m_DbContext.Todos.Where(t => t.CreatorId == userId || t.AssignedUserId == userId);

            if (filters.IsCompleted is not null)
            {
                userTodos = userTodos.Where(t => t.IsCompleted == filters.IsCompleted);
            }
            if (filters.PriorityLevels.Count > 0)
            {
                userTodos = userTodos.Where(t => filters.PriorityLevels.Contains(t.PriorityLevel));
            }

            return userTodos.ToListAsync();
        }

        public TodoEntity? Get(int todoId, bool track = true)
        {
            IQueryable<TodoEntity> todos = m_DbContext.Todos;
            if (!track)
            {
                todos = todos.AsNoTracking();
            }
            return todos.SingleOrDefault(e => e.Id == todoId);
        }

        public TodoEntity Create(TodoEntity entity)
        {
            return m_DbContext.Todos.Add(entity).Entity;
        }

        public TodoEntity Delete(TodoEntity entity)
        {
            return m_DbContext.Todos.Remove(entity).Entity;
        }

        public TodoEntity Update(TodoEntity entity)
        {
            return m_DbContext.Todos.Update(entity).Entity;
        }
    }
}
