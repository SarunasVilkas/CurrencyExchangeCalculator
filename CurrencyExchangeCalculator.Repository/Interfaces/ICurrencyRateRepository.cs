using CurrencyExchangeCalculator.Repository.Models;

namespace CurrencyExchangeCalculator.Repository.Repositories.Interfaces
{
    public interface ICurrencyRateRepository
    {
        Task<CurrencyRatesDKKModel> GetCurrencyRateForDKKByName(string currencyName);
    }
}