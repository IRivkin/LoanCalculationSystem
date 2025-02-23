namespace LoanCalculatorAPI;

public record LoanCalculationRequest(int ClientId, decimal LoanAmount, int PeriodMonths);
public record LoanCalculationResponse(LoanCalculationRequestResult Result);

public class LoanCalculatorEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/calculateloan",
            async (LoanCalculationRequest request, ISender sender) =>
            {
                try
                {
                    var command = request.Adapt<LoanCalculationCommand>();
                    // Send to LoanCalculationCommandHandler.Handle
                    var result = await sender.Send(command);
                    var response = result.Adapt<LoanCalculationResponse>();

                    return Results.Ok(response);

                }
                catch (Exception)
                {
                    throw;
                }
            });
    }
}
