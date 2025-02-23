var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(opts =>
{
    opts.Connection(Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseLoanCalculationSystem")!);
}).UseLightweightSessions();


builder.Services.InitializeMartenWith<InitialData>();

var app = builder.Build();

app.Run();
