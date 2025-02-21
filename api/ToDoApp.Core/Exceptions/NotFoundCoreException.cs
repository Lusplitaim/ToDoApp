﻿namespace ToDoApp.Core.Exceptions
{
    internal class NotFoundCoreException : RestCoreException
    {
        public NotFoundCoreException() : base()
        {
        }

        public NotFoundCoreException(string? message, Exception? inner) : base(message, inner)
        {
        }
    }
}
