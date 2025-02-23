namespace LoanCalculator.API.Models
{
    public class GlobalParameter
    {
        public int Id {  get; set; }
        public required string Name { get; set; }
        public required decimal Value { get; set; }
    }
}
;