namespace ToDoApp.Core.Exceptions
{
    internal class CoreException : Exception
    {
        public CoreException() : base()
        {
        }

        public CoreException(string? message, Exception? inner) : base(message, inner)
        {
        }
    }
}
