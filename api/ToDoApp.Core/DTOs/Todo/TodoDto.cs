using ToDoApp.Core.Data.Entities;

namespace ToDoApp.Core.DTOs.Todo
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public int CreatorId { get; set; }
        public int? AssignedUserId { get; set; }

        public static TodoDto From(TodoEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                IsCompleted = entity.IsCompleted,
                DueDate = entity.DueDate,
                Priority = entity.PriorityLevel,
                CreatorId = entity.CreatorId,
                AssignedUserId = entity.AssignedUserId,
            };
        }
    }
}
