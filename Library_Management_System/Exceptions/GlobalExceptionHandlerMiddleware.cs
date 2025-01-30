using System.Net;

namespace Library_Management_System.Exceptions
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode;
            string message;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = validationException.Message;
                    break;

                case BusinessException businessException:
                    statusCode = HttpStatusCode.Conflict;
                    message = businessException.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred. Please contact support.";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                ErrorType = exception.GetType().Name
            };

            // Log the exception (use a proper logging framework in production)
            Console.WriteLine($"Exception: {exception.Message}");

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
