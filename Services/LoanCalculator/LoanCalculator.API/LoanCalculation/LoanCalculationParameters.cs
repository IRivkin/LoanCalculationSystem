namespace LoanCalculator.API.LoanCalculation;

public class LoanCalculationParameters : ILoanCalculationParameters
{
    public LoanCalculationParameters(IGlobalParameterGetter globalParameterGetter)
    {
        MinLoanPeriodMonths = (int)globalParameterGetter.GetAsync("MinLoanPeriodMonths").Result;
        ExtraMonthInterest = 0.01m * globalParameterGetter.GetAsync("ExtraMonthInterest").Result;
        Prime = 0.01m * globalParameterGetter.GetAsync("Prime").Result;
    }

    public int MinLoanPeriodMonths { get; }
    public decimal ExtraMonthInterest { get; }
    public decimal Prime { get; }
}
