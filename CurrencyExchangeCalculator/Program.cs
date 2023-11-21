using CurrencyExchangeCalculator.Interfaces;
using CurrencyExchangeCalculator.Logic.BusinessServices;
using CurrencyExchangeCalculator.Logic.Interfaces;
using CurrencyExchangeCalculator.Repository.Repositories;
using CurrencyExchangeCalculator.Repository.Repositories.Interfaces;
using CurrencyExchangeCalculator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurrencyExchangeCalculator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddTransient<IUIService, UIService>();

            builder.Services.AddTransient<ICurrencyExchangeBusinessService, CurrencyExchangeBusinessService>();

            builder.Services.AddTransient<ICurrencyRateRepository, CurrencyRateRepository>();

            using IHost host = builder.Build();

            var svc = ActivatorUtilities.CreateInstance<UIService>(host.Services);

            await svc.InitializeUIAsync();


        }
    }
}