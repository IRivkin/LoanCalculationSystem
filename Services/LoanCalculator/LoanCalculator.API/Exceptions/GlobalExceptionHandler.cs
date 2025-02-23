namespace LoanCalculator.API.Exceptions;

public class GlobalExceptionHandler(
        IHostEnvironment Env, IFeatureManager FeatureManager, ILogger<GlobalExceptionHandler> Logger) 
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();
        problemDetails.Instance = context.Request.Path;
        problemDetails.Title = exception.Message;
        problemDetails.Status = context.Response.StatusCode;
        if (Env.IsDevelopment() && await FeatureManager.IsEnabledAsync("EnableExceptionDetails"))
        {
            problemDetails.Detail = exception.ToString();
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["data"] = exception.Data;
        }

        Logger.LogError($"GlobalExceptionHandler.TryHandleAsync {problemDetails.Title}, {problemDetails.Status}, {problemDetails.Instance}");

        await context.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
