namespace EBev.API.Extension
{
    public class UnhandledExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<UnhandledExceptionHandlerMiddleware> _logger;

        public UnhandledExceptionHandlerMiddleware(
            ILogger<UnhandledExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger?.LogDebug("Request {Method} {Path} passed through middleware {Middleware}", context.Request.Method, context.Request.Path, nameof(UnhandledExceptionHandlerMiddleware));

            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                LogException(exception, context);
                await HandleExceptionAsync(context);
            }
        }

        private void LogException(Exception exception, HttpContext context)
        {
            _logger?.LogError(exception, "An unhandled exception has occurred. Request {Method} {Path} failed with Status Code {StatusCode}. Error: {ExceptionMessage}\nStack Trace: {StackTrace}",
                context.Request.Method,
                context.Request.Path,
                HttpStatusCode.InternalServerError,
                exception.Message,
                exception.StackTrace);
        }

        private async Task HandleExceptionAsync(HttpContext context)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Exception occurred while processing request");
            }
            else
            {
                _logger?.LogWarning("Can't write error response. Response has already started.");
            }
        }
    }
}
