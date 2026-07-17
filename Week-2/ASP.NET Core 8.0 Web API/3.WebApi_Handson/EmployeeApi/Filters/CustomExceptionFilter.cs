using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeApi.Filters
{
    // Simulating ExceptionResult for .NET 8 since WebApiCompatShim is deprecated
    public class ExceptionResult : IActionResult
    {
        private readonly Exception _exception;
        public ExceptionResult(Exception exception) { _exception = exception; }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(new { error = _exception.Message }) { StatusCode = 500 };
            await result.ExecuteResultAsync(context);
        }
    }

    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionDetail = context.Exception.ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "error_log.txt");
            File.AppendAllText(filePath, $"[{DateTime.Now}] {exceptionDetail}\n");

            // Set the Result property to ExceptionResult as requested
            context.Result = new ExceptionResult(context.Exception);
            context.ExceptionHandled = true;
        }
    }
}
