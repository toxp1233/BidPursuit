using bidPursuit.Domain.Exceptions;

namespace bidPursuit.API.Exceptions;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);

            logger.LogWarning(notFound.Message);
        }
        catch (AlreadyExistsException AlreadyExists)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(AlreadyExists.Message);
            logger.LogInformation(AlreadyExists.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }

    }
}

