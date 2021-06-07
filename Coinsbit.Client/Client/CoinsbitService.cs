using Coinsbit.Client.Client.Models;
using Coinsbit.Client.Client.Models.Abstract;
using Coinsbit.Client.Client.Models.AccountBalance;
using Coinsbit.Client.Client.Models.AccountOrder;
using Coinsbit.Client.Client.Models.Converters;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coinsbit.Client.Client
{
    public class CoinsbitService
    {
        private readonly HttpClient _client;
        private readonly CoinsbitServiceOption _options;
        public CoinsbitService(HttpClient client, CoinsbitServiceOption options)
        {
            _client = client;
            _options = options;
        }


        public async Task<AccountBalanceResponseModel> GetAccountBalance(string currency = "")
        {
            var request = GetRequest(new AccountBalanceRequestModel());
            return await SendRequest<CoinsbitRequest<AccountBalanceRequestModel>, AccountBalanceResponseModel>(request, CoinsbitServiceExtension.accountBalanceEndpoint);
        }

        public async Task<AccountOrderResponseModel> SendOrderRequest()
        {
            var request = GetRequest(new AccountOrderRequestModel { Limit = 100, Offset = 10, OrderId = 1234 });
            return await SendRequest<CoinsbitRequest<AccountOrderRequestModel>, AccountOrderResponseModel>(request, CoinsbitServiceExtension.accountOrderEndpoint);
        }


        private async Task<TResponse> SendRequest<TRequest, TResponse>(TRequest request, string endpoint)
        {
            var serializerOpt = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            serializerOpt.Converters.Add(new ResponseMessageConverter());

            var requestData = JsonSerializer.Serialize(request, serializerOpt);
            var payload = Convert.ToBase64String(Encoding.ASCII.GetBytes(requestData));

            _client.DefaultRequestHeaders.Add("X-TXC-PAYLOAD", payload);
            using (var hmac512 = new HMACSHA512(Encoding.ASCII.GetBytes(_options.Secret)))
            {
                var hash = hmac512.ComputeHash(Encoding.ASCII.GetBytes(payload));
                _client.DefaultRequestHeaders.Add("X-TXC-SIGNATURE", BitConverter.ToString(hash).ToLower().Replace("-", string.Empty));
            }

            var response = await _client.PostAsync(endpoint, new StringContent(requestData, Encoding.ASCII, "application/json"));
            var s = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Response: {s}");
            return JsonSerializer.Deserialize<TResponse>(await response.Content.ReadAsStringAsync(), serializerOpt);
        }
        private CoinsbitRequest<T> GetRequest<T>(T data) where T : BaseRequestData => new CoinsbitRequest<T>(data);

    }
}
