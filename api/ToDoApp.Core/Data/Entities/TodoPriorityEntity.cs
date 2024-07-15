namespace ToDoApp.Core.Data.Entities
{
    public class TodoPriorityEntity
    {
        public int Level { get; set; }

        public ICollection<TodoEntity> Todos { get; } = [];
    }
}