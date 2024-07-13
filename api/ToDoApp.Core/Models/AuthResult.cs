using ToDoApp.Core.DTOs.User;

namespace ToDoApp.Core.Models
{
    public class AuthResult
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
