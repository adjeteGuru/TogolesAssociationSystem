using FluentValidation;
using TogoleseSolidarity.API.Exceptions;
namespace TogoleseSolidarity.API.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            ResetResponse(context.Response, StatusCodes.Status400BadRequest, ex);
            LogValidationErroCodes(ex);
        }
        catch (NotFoundException ex)
        {
            ResetResponse(context.Response, StatusCodes.Status404NotFound, ex);
            Log(LogLevel.Warning, ex);
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

    private void LogValidationErroCodes(ValidationException exception)
    {
        var errorCodes = exception.Errors.Select(e => $"{e.PropertyName}: {e.ErrorCode}");
        var formattedErrorCodes = string.Join(", ", errorCodes);
        const string logPattern = "{message}, ErrorCodes:- {errorCodes}";
        logger.Log(LogLevel.Warning, exception, logPattern, exception.Message, formattedErrorCodes);
        logger.LogDebug(exception.StackTrace);
    }
}
