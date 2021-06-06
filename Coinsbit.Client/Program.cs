using Coinsbit.Client.Client;
using Microsoft.AspNetCore.Http;
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

            var url = configuration[$"{nameof(CoinbitServiceOption)}:Url"];
            var serviceProvider = new ServiceCollection()
                .AddHttpClient<CoinbitService>(client =>
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("X-TXC-APIKEY", "");
            }).Services.BuildServiceProvider();

            var client = serviceProvider.GetService<CoinbitService>();
            Console.Write(@"text currency:");
            var cur = Console.ReadLine();
            var response = await client.GetAccountBalance(cur);

            Console.ReadKey();
        }


    }
}
