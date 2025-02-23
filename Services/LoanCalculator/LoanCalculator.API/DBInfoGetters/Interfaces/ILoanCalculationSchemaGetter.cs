namespace LoanCalculator.API.DBInfoGetters.Interfaces;

public interface ILoanCalculationSchemaGetter
{
    ValueTask<LoanCalculationSchema> GetAsync(int clientAge, decimal loanAmount);
}
