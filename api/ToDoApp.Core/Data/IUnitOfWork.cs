namespace ToDoApp.Core.Data
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
