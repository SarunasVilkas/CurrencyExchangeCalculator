namespace CurrencyExchangeCalculator.Logic.Models
{
    public class CalculateCurrencyRequest
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal ExchangeAmount { get; set; }
    }
}
