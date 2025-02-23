namespace LoanCalculatorAPI;

public record LoanCalculationCommand(int ClientId, decimal LoanAmount, int PeriodMonths)
        : IRequest<LoanCalculationCommandResult>;
public record LoanCalculationCommandResult(LoanCalculationRequestResult Result);

public class LoanCalculationCommandHandler(ILoanCalculation LoanCalculation)
    : IRequestHandler<LoanCalculationCommand, LoanCalculationCommandResult>
{
    public async Task<LoanCalculationCommandResult> Handle(LoanCalculationCommand command, CancellationToken cancellationToken)
    {
        var totalAmount = await LoanCalculation.Calculate(command.ClientId, command.LoanAmount, command.PeriodMonths);

        var result = new LoanCalculationRequestResult
        {
            ClientId = command.ClientId,
            LoanAmount = command.LoanAmount,
            TotalAmount = totalAmount,
            PeriodMonths = command.PeriodMonths
        };

        return new LoanCalculationCommandResult(result);
    }
}
