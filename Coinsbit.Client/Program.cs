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

            var options = configuration.GetSection(nameof(CoinsbitServiceOption)).Get<CoinsbitServiceOption>();
            var serviceProvider = new ServiceCollection()
                .AddSingleton(options)
                .AddHttpClient<CoinsbitService>(client =>
            {
                client.BaseAddress = new Uri(options.Url);
                client.DefaultRequestHeaders.Add("X-TXC-APIKEY", options.Key);
            }).Services.BuildServiceProvider();

            var client = serviceProvider.GetService<CoinsbitService>();
            Console.Write(@"text currency:");
            //var cur = Console.ReadLine();
            //var response1 = await client.GetAccountBalance();
            var response2 = await client.SendOrderRequest();


            Console.ReadKey();
        }


    }
}
