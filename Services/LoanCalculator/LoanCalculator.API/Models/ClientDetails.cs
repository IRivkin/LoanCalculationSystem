namespace LoanCalculator.API.Models;

public record ClientDetails
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Age { get; set; }
}
