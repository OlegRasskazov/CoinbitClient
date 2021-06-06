using Coinsbit.Client.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Coinsbit.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var options = configuration.GetSection(nameof(CoinbitServiceOption)).Get<CoinbitServiceOption>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(options)
                .AddHttpClient<CoinbitService>(client =>
            {
                client.BaseAddress = new Uri(options.Url);
                client.DefaultRequestHeaders.Add("X-TXC-APIKEY", options.Key);
            }).Services.BuildServiceProvider();

            var client = serviceProvider.GetService<CoinbitService>();
            Console.Write(@"text currency:");
            var cur = Console.ReadLine();
            var response = await client.GetAccountBalance(cur);

            Console.ReadKey();
        }


    }
}
