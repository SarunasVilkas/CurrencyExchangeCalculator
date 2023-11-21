using CurrencyExchangeCalculator.Repository.Repositories;

namespace CurrencyExchangeCalculator.UnitTests.Repository
{
    public class CurrencyRateRepositoryUnitTests
    {
        [Test]
        public void GetCurrencyRateForDKKByName_ReturnsProperValue()
        {
            var testObject = new CurrencyRateRepository();

            var response = testObject.GetCurrencyRateForDKKByName("EUR");

            Assert.That(response.Result.Rate, Is.EqualTo(7.4394m));
        }

        [Test]
        public void GetCurrencyRateForDKKByName_CurrencyIsNotInDictionary_ReturnsNull()
        {
            var testObject = new CurrencyRateRepository();

            var response = testObject.GetCurrencyRateForDKKByName("FAKE");

            Assert.That(response.Result, Is.Null);
        }
    }
}
