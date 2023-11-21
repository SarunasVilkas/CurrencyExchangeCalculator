using CurrencyExchangeCalculator.Interfaces;
using CurrencyExchangeCalculator.Logic.Interfaces;
using CurrencyExchangeCalculator.Logic.Models;
using System.Text.RegularExpressions;

namespace CurrencyExchangeCalculator.Services
{
    public class UIService : IUIService
    {
        private readonly ICurrencyExchangeBusinessService _currencyExchangeBusinessService;

        public UIService(ICurrencyExchangeBusinessService currencyExchangeBusinessService)
        {
            _currencyExchangeBusinessService = currencyExchangeBusinessService;
        }

        public async Task InitializeUIAsync()
        {
            await Console.Out.WriteLineAsync("Usage: Exchange <currency from/currency to> <amount to exchange> e.g. ABC/DEF 1,1");
            await Console.Out.WriteLineAsync("Press Ctrl+C to close application");

            do
            {
                await Console.Out.WriteAsync("Exchange ");
                var input = await Console.In.ReadLineAsync();

                if (!IsUserInputValid(input))
                {
                    await Console.Out.WriteLineAsync("Input is invalid. Check input format and try again.");
                }
                else
                {
                    var request = CreateRequestOfUserInput(input);

                    if (request.FromCurrency.Equals(request.ToCurrency))
                    {
                        await Console.Out.WriteLineAsync("Input is invalid. The same currency entered.");
                        continue;
                    }

                    var result = await _currencyExchangeBusinessService.CalculateCurrencyAsync(request);

                    if (result == decimal.Zero)
                    {
                        await Console.Out.WriteLineAsync("Error: Currency exchange calculation failed. Provided currency not found.");
                    }
                    else if (result == -1)
                    {
                        await Console.Out.WriteLineAsync("Error: Currency exchange calculation failed. Bad request.");
                    }
                    else
                    {
                        await Console.Out.WriteLineAsync($"{result}");
                    }
                }

            } while (true);
        }

        private static CalculateCurrencyRequest CreateRequestOfUserInput(string input)
        {
            return new CalculateCurrencyRequest
            {
                FromCurrency = input.Substring(0, 3).ToUpper(),
                ToCurrency = input.Substring(4, 3).ToUpper(),
                ExchangeAmount = decimal.Parse(input.Substring(8))
            };
        }

        private static bool IsUserInputValid(string input)
        {
            const string pattern = @"^[A-Za-z]{3}\/[A-Za-z]{3}\s\d+([,]\d+)?$";

            return Regex.IsMatch(input, pattern);
        }
    }
}
