namespace LoanCalculator.API.DBInfoGetters.Interfaces;

public interface IClientDetailsGetter
{
    ValueTask<ClientDetails> GetAsync(int id);
}
