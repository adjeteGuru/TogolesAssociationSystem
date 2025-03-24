namespace TogoleseSolidarity.API.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ResetResponse(context.Response, StatusCodes.Status500InternalServerError, ex);
                Log(LogLevel.Error, ex);
            }
        }

        private void ResetResponse(HttpResponse response, int statusCode, Exception exception)
        {
            if (response.HasStarted)
            {
                logger.LogWarning("The response has already started, the error handler will not be executed.");
                throw exception;
            }
            response.Clear();
            response.StatusCode = statusCode;
        }

        private void Log(LogLevel level, Exception exception)
        {
            logger.Log(level, exception, exception.Message);
            logger.LogDebug(exception.StackTrace);
        }
    }
}
