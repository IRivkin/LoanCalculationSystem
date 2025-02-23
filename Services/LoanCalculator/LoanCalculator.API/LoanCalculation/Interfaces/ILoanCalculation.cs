namespace LoanCalculator.API.LoanCalculation.Interfaces;

public interface ILoanCalculation
{
    ValueTask<decimal> Calculate(int clientId, decimal loanAmount, int loanPeriodMonth);
}
