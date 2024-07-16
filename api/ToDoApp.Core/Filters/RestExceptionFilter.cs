﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoApp.Core.Exceptions;

namespace ToDoApp.Core.Filters
{
    public class RestExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            IActionResult? result = null;

            switch (context.Exception)
            {
                case NotFoundCoreException:
                    result = new NotFoundResult();
                    break;
                case ForbiddenCoreException:
                    result = new ForbidResult();
                    break;
            }

            if (result is not null)
            {
                context.Result = result;
                context.ExceptionHandled = true;
            }
        }
    }
}
