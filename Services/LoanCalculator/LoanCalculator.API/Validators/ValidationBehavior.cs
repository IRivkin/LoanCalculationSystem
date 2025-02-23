namespace LoanCalculator.API.Validators;

/// <summary>
/// Checking a transaction just before processing
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <param name="validators"></param>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> Validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults =
            await Task.WhenAll(Validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures =
            validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}
