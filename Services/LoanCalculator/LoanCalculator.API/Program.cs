using LoanCalculator.API.DBInfoGetters;
using LoanCalculator.API.LoanCalculation;

var builder = WebApplication.CreateBuilder(args);
var swaggerEnabled = false; 

AddCarter(builder);
AddValidators();
AddMeiatR(builder);
AddMarten(builder);
AddHealthCheck(builder);
AddExceptionHandler(builder);
AddFeatureManagement(builder);
AddServices(builder);
AddCors(builder);
AddSwagger(builder);

var app = builder.Build();

UseCarter(app);
UseExceptionHandler(app);
UseHealthChecks(app);
UseCors(app);
UseSwagger(app);

app.Run();

void AddCarter(WebApplicationBuilder builder) => builder.Services.AddCarter();

void AddValidators() => builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

void AddMeiatR(WebApplicationBuilder builder)
{
    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });
}

void AddMarten(WebApplicationBuilder builder)
{
    builder.Services.AddMarten(opts =>
    {
        opts.Connection(Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseLoanCalculationSystem")!);
    }).UseLightweightSessions();
}

void AddHealthCheck(WebApplicationBuilder builder)
{
    builder.Services.AddHealthChecks()
        .AddNpgSql(Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseLoanCalculationSystem")!);
}

void AddExceptionHandler(WebApplicationBuilder builder)
{
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
}

void AddFeatureManagement(WebApplicationBuilder builder) => builder.Services.AddFeatureManagement();

void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IGlobalParameterGetter, GlobalParameterGetter>();
    builder.Services.AddSingleton<ILoanCalculationSchemaGetter, LoanCalculationSchemaGetter>();
    builder.Services.AddSingleton<IClientDetailsGetter, ClientDetailsGetter>();
    builder.Services.AddSingleton<ILoanCalculation, LoanCalculation>();
    builder.Services.AddSingleton<ILoanCalculationParameters, LoanCalculationParameters>();
}

void AddCors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });
}

void AddSwagger(WebApplicationBuilder builder)
{
    swaggerEnabled = builder.Configuration.GetValue<bool>("FeatureManagement:EnableSwagger");
    if (swaggerEnabled)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}

void UseCarter(WebApplication app) => app.MapCarter();

void UseExceptionHandler(WebApplication app) => app.UseExceptionHandler();

void UseHealthChecks(WebApplication app)
{
    app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
}

void UseCors(WebApplication app) => app.UseCors("AllowAll");

void UseSwagger(WebApplication app)
{
    if (swaggerEnabled && app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.DefaultModelsExpandDepth(-1);// Schema filter is useless 
            options.RoutePrefix = string.Empty;
        });
    }
}

