using CurrencyExchangeCalculator.Repository.Models;
using CurrencyExchangeCalculator.Repository.Repositories.Interfaces;

namespace CurrencyExchangeCalculator.Repository.Repositories
{
    public class CurrencyRateRepository : ICurrencyRateRepository
    {
        public Task<CurrencyRatesDKKModel> GetCurrencyRateForDKKByName(string currencyName)
        {

            bool IsCurrencyRateAvailable = currencyRatesForDKK.TryGetValue(currencyName, out decimal rate);

            if (!IsCurrencyRateAvailable)
            {
                return Task.FromResult<CurrencyRatesDKKModel>(null);
            }

            return Task.FromResult(new CurrencyRatesDKKModel
            {
                Name = currencyName,
                Rate = rate
            });
        }

        private readonly Dictionary<string, decimal> currencyRatesForDKK = new()
        {
                {"DKK", 1.0000m},
                {"EUR", 7.4394m},
                {"USD", 6.6311m},
                {"GBP",  8.5285m},
                {"SEK",  0.7610m},
                {"NOK",  0.7840m},
                {"CHF",  6.8358m},
                {"JPY",  0.0597m}
        };
    }
}
