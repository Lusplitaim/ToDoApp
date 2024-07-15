namespace ToDoApp.Core.Models
{
    public class TodoFilters
    {
        public bool? IsCompleted { get; set; }
        public HashSet<int> PriorityLevels { get; set; } = [];
    }
}
