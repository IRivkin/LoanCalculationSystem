namespace CreateTestData.InitialData;

public class InitialData : IInitialData
{
    /// <summary>
    /// This method creates tables:
    ///     - GlobalParameter
    ///     - LoanCalculationSchema
    ///     - ClientDetails
    /// </summary>
    /// <param name="store"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (!await session.Query<GlobalParameter>().AnyAsync())
        {
            session.Store<GlobalParameter>(GetPreconfiguredGlobalParameters());
            await session.SaveChangesAsync();
        }

        if (!await session.Query<LoanCalculationSchema>().AnyAsync())
        {
            session.Store<LoanCalculationSchema>(GetPreconfiguredLoanCalculationSchema());
            await session.SaveChangesAsync();
        }

        if (!await session.Query<ClientDetails>().AnyAsync())
        {
            session.Store<ClientDetails>(GetPreconfiguredClientDetailes());
            await session.SaveChangesAsync();
        }
    }

    private static IEnumerable<GlobalParameter> GetPreconfiguredGlobalParameters() => new List<GlobalParameter>
    {
        new GlobalParameter
        {
            Name = "MinLoanPeriodMonths",
            Value = 12
        },
        new GlobalParameter
        {
            Name = "Prime",
            Value = 1.6m
        },
        new GlobalParameter
        {
            Name = "ExtraMonthInterest",
            Value = 0.15m
        }
    };

    private static IEnumerable<LoanCalculationSchema> GetPreconfiguredLoanCalculationSchema() => new List<LoanCalculationSchema>
    {
        new LoanCalculationSchema
        {
            MaxAge = 19,
            MaxLoan = 5000,
            Interest = 2,
            UsePrime = false
        },
        new LoanCalculationSchema
        {
            MaxAge = 35,
            MaxLoan = 5000,
            Interest = 2,
            UsePrime = false
        },
        new LoanCalculationSchema
        {
            MaxAge = 35,
            MaxLoan = 30000,
            Interest = 1.5m,
            UsePrime = true
        },
        new LoanCalculationSchema
        {
            MaxAge = 35,
            MaxLoan = decimal.MaxValue,
            Interest = 1,
            UsePrime = true
        },
        new LoanCalculationSchema
        {
            MaxAge = 35,
            MaxLoan = 5000,
            Interest = 1.5m,
            UsePrime = true
        },
        new LoanCalculationSchema
        {
            MaxAge = 35,
            MaxLoan = 30000,
            Interest = 3,
            UsePrime = true
        },
        new LoanCalculationSchema
        {
            MaxAge = 35,
            MaxLoan = decimal.MaxValue,
            Interest = 1,
            UsePrime = false
        }
    };

    private static IEnumerable<ClientDetails> GetPreconfiguredClientDetailes() => new List<ClientDetails>
    {
        new ClientDetails
        {
            Id = 118,
            FirstName = "FirstName118",
            LastName = "LastName118",
            Age = 18
        },
        new ClientDetails
        {
            Id = 119,
            FirstName = "FirstName119",
            LastName = "LastName119",
            Age = 19
        },
        new ClientDetails
        {
            Id = 120,
            FirstName = "FirstName120",
            LastName = "LastName120",
            Age = 20
        },
        new ClientDetails
        {
            Id = 125,
            FirstName = "FirstName125",
            LastName = "LastName125",
            Age = 25
        },
        new ClientDetails
        {
            Id = 135,
            FirstName = "FirstName135",
            LastName = "LastName135",
            Age = 35
        },
        new ClientDetails
        {
            Id = 140,
            FirstName = "FirstName140",
            LastName = "LastName140",
            Age = 40
        },
    };
}
