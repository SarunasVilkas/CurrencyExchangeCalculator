using CurrencyExchangeCalculator.Logic.Models;

namespace CurrencyExchangeCalculator.Logic.Interfaces
{
    public interface ICurrencyExchangeBusinessService
    {
        Task<decimal> CalculateCurrencyAsync(CalculateCurrencyRequest request);
    }
}
