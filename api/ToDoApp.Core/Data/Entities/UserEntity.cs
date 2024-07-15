using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Core.Data.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public override string UserName { get; set; }
        public override string Email { get; set; }

        public ICollection<TodoEntity> Todos { get; } = [];
    }
}
