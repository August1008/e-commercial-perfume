using Core.Models.ErrorModels;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ErrorHandlingMiddleware(IHostEnvironment env, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                await HandleErrorAsync(context, ex, env);
            }
        }

        private Task HandleErrorAsync(HttpContext context, Exception ex, IHostEnvironment env)
        {
            Console.WriteLine(ex.StackTrace);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new ErrorResponse(context.Response.StatusCode, "Internal Server Error", ex.ToString());
           
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
