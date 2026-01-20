using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace eCommerce.ProductsMicroservice.API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionalHandlingMiDDleWare
    {
        private readonly RequestDelegate _next;
        public readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionalHandlingMiDDleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //log the exception type and message
                _logger.LogError($"{ex.GetType().ToString()}:{ex.Message}");
                if (ex.InnerException is not null)
                {
                    _logger.LogError($"{ex.GetType().ToString()}:{ex.Message}");

                }
                httpContext.Response.StatusCode = 500; //internak SeRver ErRor
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = ex.Message,
                    Type = ex.GetType().ToString()
                });
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionalHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionalHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionalHandlingMiDDleWare>();
        }
    }
}
