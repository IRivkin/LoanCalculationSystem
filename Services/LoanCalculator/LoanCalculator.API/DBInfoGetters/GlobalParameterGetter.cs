namespace LoanCalculator.API.DBInfoGetters;

public class GlobalParameterGetter : IGlobalParameterGetter
{
    public async Task<decimal> GetAsync(string name)
    {
        using var store = DocumentStore.For(opts =>
        {
            opts.Connection(Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseLoanCalculationSystem")!);
        });

        await using var session = store.QuerySession();

        var globalParameter = await session.Query<GlobalParameter>()
            .Where(x => x.Name.Equals(name))
            .FirstOrDefaultAsync();

        if (globalParameter is null)
        {
            throw new NullReferenceException($"GlobalParameter {name} not found");
        }

        return globalParameter!.Value;
    }
}
