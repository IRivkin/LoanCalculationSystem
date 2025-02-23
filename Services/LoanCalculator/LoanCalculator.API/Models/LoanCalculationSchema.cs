namespace LoanCalculator.API.Models;

public record LoanCalculationSchema
{
    public int Id { get; set; }
    public required int MaxAge { get; set; }
    public required decimal MaxLoan { get; set; }
    public required decimal Interest { get; set; }
    public required bool UsePrime { get; set; }
}
