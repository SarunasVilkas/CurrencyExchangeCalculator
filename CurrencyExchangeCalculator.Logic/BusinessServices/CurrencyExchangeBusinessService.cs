using CurrencyExchangeCalculator.Logic.Interfaces;
using CurrencyExchangeCalculator.Logic.Models;
using CurrencyExchangeCalculator.Repository.Repositories.Interfaces;

namespace CurrencyExchangeCalculator.Logic.BusinessServices
{
    public class CurrencyExchangeBusinessService : ICurrencyExchangeBusinessService
    {
        private readonly ICurrencyRateRepository _currencyRateRepository;

        public CurrencyExchangeBusinessService(ICurrencyRateRepository currencyRateRepository)
        {
            _currencyRateRepository = currencyRateRepository;
        }

        public async Task<decimal> CalculateCurrencyAsync(CalculateCurrencyRequest request)
        {
            if (request == null)
            {
                return -1;
            }

            var fromCurrencyRate = await _currencyRateRepository.GetCurrencyRateForDKKByName(request.FromCurrency);
            var toCurrencyRate = await _currencyRateRepository.GetCurrencyRateForDKKByName(request.ToCurrency);

            if (fromCurrencyRate == null || toCurrencyRate == null)
            {
                return decimal.Zero;
            }

            return fromCurrencyRate.Rate / toCurrencyRate.Rate * request.ExchangeAmount;
        }
    }
}
