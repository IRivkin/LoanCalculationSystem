namespace LoanCalculator.API.DBInfoGetters.Interfaces;

public interface IGlobalParameterGetter
{
    Task<decimal> GetAsync(string name);
}
