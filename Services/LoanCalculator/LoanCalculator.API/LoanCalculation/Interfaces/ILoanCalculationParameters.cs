namespace LoanCalculator.API.LoanCalculation.Interfaces;

public interface ILoanCalculationParameters
{
    int MinLoanPeriodMonths { get; }
    decimal ExtraMonthInterest { get; }
    decimal Prime { get; }
}
