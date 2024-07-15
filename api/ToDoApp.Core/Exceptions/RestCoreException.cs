namespace ToDoApp.Core.Exceptions
{
    internal class RestCoreException : CoreException
    {
        public RestCoreException() : base()
        {
        }

        public RestCoreException(string? message, Exception? inner) : base(message, inner)
        {
        }
    }
}
