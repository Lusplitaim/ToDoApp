using FluentValidation;
using ToDoApp.Core.Data;
using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.Models;

namespace ToDoApp.Core.Storages.Validators
{
    internal class EditTodoValidator : AbstractValidator<ModificationModel<TodoEntity>>
    {
        private readonly IUnitOfWork m_UnitOfWork;
        public EditTodoValidator(IUnitOfWork uow)
        {
            m_UnitOfWork = uow;

            RuleFor(todo => (TodoPriority)todo.Current.PriorityLevel).IsInEnum().WithMessage("The priority level does not exist");
            RuleFor(todo => todo.Current.AssignedUserId).Must(UserExists).WithMessage("The assigned user does not exist");
        }

        private bool UserExists(int? assignedUserId)
        {
            if (assignedUserId is null)
            {
                return true;
            }

            var user = m_UnitOfWork.UserRepository.Get(assignedUserId.Value);

            return user is not null;
        }
    }
}
