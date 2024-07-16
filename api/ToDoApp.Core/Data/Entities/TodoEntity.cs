namespace ToDoApp.Core.Data.Entities
{
    public class TodoEntity : ICloneable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }

        public int PriorityLevel { get; set; }
        public TodoPriorityEntity Priority { get; set; }

        public int CreatorId { get; set; }
        public UserEntity Creator { get; set; }

        public int? AssignedUserId { get; set; }
        public UserEntity? AssignedUser { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
