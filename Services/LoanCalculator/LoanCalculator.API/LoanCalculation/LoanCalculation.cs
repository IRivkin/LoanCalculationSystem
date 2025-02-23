namespace LoanCalculator.API.LoanCalculation;

public class LoanCalculation(ILoanCalculationParameters LoanCalculationParameters, ILoanCalculationSchemaGetter LoanCalculationSchemaGetter,
        IClientDetailsGetter ClientDetailsGetter) : ILoanCalculation
{
    public async ValueTask<decimal> Calculate(int clientId, decimal loanAmount, int loanPeriodMonth)
    {
        var clientDetails = await ClientDetailsGetter.GetAsync(clientId);
        var loanCalculationSchema = 
            await LoanCalculationSchemaGetter.GetAsync(clientDetails.Age, loanAmount);

        var totalAmount = 0m;
        if (loanPeriodMonth > LoanCalculationParameters.MinLoanPeriodMonths)
        {
            totalAmount = loanAmount * LoanCalculationParameters.ExtraMonthInterest
                * (loanPeriodMonth - LoanCalculationParameters.MinLoanPeriodMonths);
        }

        var interest = loanCalculationSchema.Interest / 100;
        if (loanCalculationSchema.UsePrime)
        {
            interest += LoanCalculationParameters.Prime;
        }

        totalAmount += loanAmount + loanAmount * interest;

        return totalAmount;
    }
}
