namespace ToDoApp.Core.Models
{
    internal class ModificationModel<T> where T : class
    {
        public T Current { get; }
        public T? Prev { get; }

        public ModificationModel(T? prev, T current)
        {
            Prev = prev;
            Current = current;
        }
    }
}
