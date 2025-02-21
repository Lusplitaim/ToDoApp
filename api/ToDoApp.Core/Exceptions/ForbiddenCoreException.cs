﻿namespace ToDoApp.Core.Exceptions
{
    internal class ForbiddenCoreException : RestCoreException
    {
        public ForbiddenCoreException() : base()
        {
        }

        public ForbiddenCoreException(string? message, Exception? inner) : base(message, inner)
        {
        }
    }
}
