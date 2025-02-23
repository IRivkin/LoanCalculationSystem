namespace LoanCalculator.API.DBInfoGetters;

public class LoanCalculationSchemaGetter : ILoanCalculationSchemaGetter
{
    public async ValueTask<LoanCalculationSchema> GetAsync(int clientAge, decimal loanAmount)
    {
        using var store = DocumentStore.For(opts =>
        {
            opts.Connection(Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseLoanCalculationSystem")!);
        });

        await using var session = store.QuerySession();

        var loanCalculationSchema = await session.Query<LoanCalculationSchema>()
            .Where(x => x.MaxAge >= clientAge
                && x.MaxLoan >= loanAmount)
            .OrderBy(x => x.MaxAge)
            .OrderBy(x => x.MaxLoan)
            .FirstOrDefaultAsync();

        return loanCalculationSchema!;
    }
}
