using LoanCalculatorAPI;

namespace LoanCalculator.API.Validators;

public class LoanCalculationRequestValidator : AbstractValidator<LoanCalculationCommand>
{
    private readonly ILoanCalculationParameters _loanCalculationParameters;
    private readonly IClientDetailsGetter _clientDetailsGetter;
    private readonly ILogger<LoanCalculationRequestValidator> _logger;

    /// <summary>
    /// Checking a transaction just before processing
    /// </summary>
    /// <param name="clientDetailsGetter"></param>
    /// <param name="logger"></param>
    public LoanCalculationRequestValidator(
        ILoanCalculationParameters loanCalculationParameters, IClientDetailsGetter clientDetailsGetter,
        ILogger<LoanCalculationRequestValidator> logger)
    {
        _loanCalculationParameters = loanCalculationParameters;
        _clientDetailsGetter = clientDetailsGetter;
        _logger = logger;

        Validate();
    }

    private void Validate()
    {
        RuleFor(x => x).MustAsync(async (loanCalculationCommand, token) =>
        {
            return await ValidateClient(loanCalculationCommand.ClientId);
        }).WithMessage("Client not found");

        RuleFor(x => x).Must((loanCalculationCommand) =>
        {
            return ValidateLoanPeriod(loanCalculationCommand.PeriodMonths);
        }).WithMessage($"Loan period less than {_loanCalculationParameters.MinLoanPeriodMonths}");

        RuleFor(x => x).Must((loanCalculationCommand) =>
        {
            return ValidateLoanAmount(loanCalculationCommand.LoanAmount);
        }).WithMessage("The value of loan amount is not positive");
    }

    private async ValueTask<bool> ValidateClient(int clientId)
    {
        var clientDetails = await _clientDetailsGetter.GetAsync(clientId);

        if (clientDetails is not null)
            return true;

        _logger.LogError($"LoanCalculationRequestValidator.ValidateClient. Client ID {clientId} not found");

        return false;
    }

    private bool ValidateLoanPeriod(int loanPeriod)
    {
        if (loanPeriod >= _loanCalculationParameters.MinLoanPeriodMonths)
            return true;

        _logger.LogError(
            @$"LoanCalculationRequestValidator.ValidateLoanPeriod. The loan period must be at least {_loanCalculationParameters.MinLoanPeriodMonths} months,
            but {loanPeriod} months were requested");

        return false;
    }

    private bool ValidateLoanAmount(decimal loanAmount)
    {
        if (loanAmount > 0)
            return true;

        _logger.LogError(
            $"LoanCalculationRequestValidator.ValidateLoanAmount. The value of loan amount is not positive {loanAmount}");

        return false;
    }
}
