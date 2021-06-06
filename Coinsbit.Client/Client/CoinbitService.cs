using Coinsbit.Client.Client.Models;
using Coinsbit.Client.Client.Models.Abstract;
using Coinsbit.Client.Client.Models.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coinsbit.Client.Client
{
    public class CoinbitService
    {
        private readonly HttpClient _client;
        private readonly CoinbitServiceOption _options;
        public CoinbitService(HttpClient client, CoinbitServiceOption options)
        {
            _client = client;
            _options = options;
        }


        public async Task<AccountBalanceResponseModel> GetAccountBalance(string currency)
        {
            var request = GetRequest(new AccountBalanceRequestModel());
            return await SendRequest<CoinbitRequest<AccountBalanceRequestModel>, AccountBalanceResponseModel>(request);
        }


        private async Task<TResponse> SendRequest<TRequest, TResponse>(TRequest request)
        {
            var serializerOpt = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var requestData = JsonSerializer.Serialize(request, serializerOpt);
            var requestBytes = System.Text.Encoding.UTF8.GetBytes(requestData);
            var secretBytes = System.Text.Encoding.UTF8.GetBytes(_options.Secret);
            var payload = Convert.ToBase64String(requestBytes);
            _client.DefaultRequestHeaders.Add("X-TXC-PAYLOAD", payload);
            using (var hmac512 = new HMACSHA512(secretBytes))
            {
                var hash = hmac512.ComputeHash(requestBytes);
                _client.DefaultRequestHeaders.Add("X-TXC-SIGNATURE", BitConverter.ToString(hash).ToLower().Replace("-", string.Empty));
            }

            var response = await _client.PostAsync(CoinbitServiceExtension.accountBalanceEndpoint, new StringContent(requestData));
            var s = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Response: {s}");
            serializerOpt.Converters.Add(new ResponseMessageConverter());
            return JsonSerializer.Deserialize<TResponse>(await response.Content.ReadAsStringAsync(), serializerOpt);
        }
        private CoinbitRequest<T> GetRequest<T>(T data) where T : BaseRequestData => new CoinbitRequest<T>(data);

    }
}
