using System.Net;

namespace FirstApi.MiddleWare
{
    public class MiddleWareAuth
    {
        private readonly RequestDelegate _next;

        public MiddleWareAuth(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle the exception and log it
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // You can customize the error response here
            var errorResponse = new
            {
                Message = "An error occurred while processing your request.",
                ExceptionMessage = exception.Message+" - "+exception.InnerException,
                StackTrace = exception.StackTrace
            };

            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
