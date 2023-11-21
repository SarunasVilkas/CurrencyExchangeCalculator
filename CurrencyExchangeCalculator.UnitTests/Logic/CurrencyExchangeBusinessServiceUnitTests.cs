using CurrencyExchangeCalculator.Logic.BusinessServices;
using CurrencyExchangeCalculator.Logic.Models;
using CurrencyExchangeCalculator.Repository.Repositories.Interfaces;
using CurrencyExchangeCalculator.Repository.Models;
using Moq;

namespace CurrencyExchangeCalculator.UnitTests.Logic
{
    public class CurrencyExchangeBusinessServiceUnitTests
    {
        private Mock<ICurrencyRateRepository> _mockCurrencyRateRepository;

        [SetUp]
        public void Setup()
        {
            _mockCurrencyRateRepository = new Mock<ICurrencyRateRepository>();
        }

        private CurrencyExchangeBusinessService InitializeCurrencyExchangeBusinessService()
        {
            return new CurrencyExchangeBusinessService(_mockCurrencyRateRepository.Object);
        }

        [Test]
        public void CalculateCurrencyAsync_ReturnsProperResponse()
        {
            _mockCurrencyRateRepository.SetupSequence(i => i.GetCurrencyRateForDKKByName(It.IsAny<string>()))
                .ReturnsAsync(new CurrencyRatesDKKModel
                {
                    Name = "TestCurrencyOne",
                    Rate = 10.0m
                })
                .ReturnsAsync(new CurrencyRatesDKKModel
                {
                    Name = "TestCurrencyTwo",
                    Rate = 5.0m
                });


            var testObject = InitializeCurrencyExchangeBusinessService();

            var request = new CalculateCurrencyRequest
            {
                FromCurrency = "TestCurrencyOne",
                ToCurrency = "TestCurrencyTwo",
                ExchangeAmount = 1m
            };

            var response = testObject.CalculateCurrencyAsync(request);

            Assert.That(response.Result, Is.EqualTo(2m));
        }

        [Test]
        public void CalculateCurrencyAsync_ReturnsMinusOne_RequestIsNull()
        {
            var testObject = InitializeCurrencyExchangeBusinessService();

            var response = testObject.CalculateCurrencyAsync(null);

            Assert.That(response.Result, Is.EqualTo(-1));
        }

        [Test]
        public void CalculateCurrencyAsync_ReturnsZero_OneofRepositoryResponseIsNull()
        {
            _mockCurrencyRateRepository.SetupSequence(i => i.GetCurrencyRateForDKKByName(It.IsAny<string>()))
                .ReturnsAsync(new CurrencyRatesDKKModel
                {
                    Name = "TestCurrencyOne",
                    Rate = 10.0m
                })
                .ReturnsAsync((CurrencyRatesDKKModel)null);


            var testObject = InitializeCurrencyExchangeBusinessService();

            var request = new CalculateCurrencyRequest
            {
                FromCurrency = "TestCurrencyOne",
                ToCurrency = "TestCurrencyTwo",
                ExchangeAmount = 1m
            };

            var response = testObject.CalculateCurrencyAsync(request);

            Assert.That(response.Result, Is.EqualTo(0));
        }
    }
}
