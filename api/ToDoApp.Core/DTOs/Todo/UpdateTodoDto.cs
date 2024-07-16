using ToDoApp.Core.Models;

namespace ToDoApp.Core.DTOs.Todo
{
    public class UpdateTodoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TodoPriority Priority { get; set; }
        public int? AssignedUserId { get; set; }
    }
}
