namespace LoanCalculator.API.DBInfoGetters;

public class ClientDetailsGetter : IClientDetailsGetter
{
    public async ValueTask<ClientDetails> GetAsync(int clientId)
    {
        using var store = DocumentStore.For(opts =>
        {
            opts.Connection(Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseLoanCalculationSystem")!);
        });

        await using var session = store.QuerySession();

        var clientDetails = await session.Query<ClientDetails>()
            .Where(x => x.Id == clientId)
            .FirstOrDefaultAsync();

        return clientDetails!;
    }
}
