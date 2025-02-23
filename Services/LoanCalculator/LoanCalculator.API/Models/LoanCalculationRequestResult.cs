namespace LoanCalculator.API.Models;

public record LoanCalculationRequestResult
{
    public required int ClientId { get; set; }
    public required decimal LoanAmount { get; set; }
    public required decimal TotalAmount { get; set; }
    public required int PeriodMonths { get; set; }
}
