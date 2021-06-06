using Coinsbit.Client.Client.Models;
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
        public CoinbitService(HttpClient client) =>
            _client = client;


        public async Task<AccountBalanceResponseModel> GetAccountBalance(string currency)
        {
            var request = new AccountBalanceRequestModel();
            return await SendRequest<AccountBalanceRequestModel, AccountBalanceResponseModel>(request);
        }


        private async Task<TResponse> SendRequest<TRequest, TResponse>(TRequest request)
        {
            var requestData = JsonSerializer.Serialize(request);
            var requestBytes = System.Text.Encoding.UTF8.GetBytes(requestData);
            _client.DefaultRequestHeaders.TryGetValues("X-TXC-APIKEY", out var xKey);
            var keyBytes = System.Text.Encoding.UTF8.GetBytes(xKey.FirstOrDefault() ?? "");
            var payload = Convert.ToBase64String(requestBytes);
            _client.DefaultRequestHeaders.Add("X-TXC-PAYLOAD", payload);
            using (var hmac512 =new HMACSHA512(keyBytes))
            {
                var hash = hmac512.ComputeHash(requestBytes);
                _client.DefaultRequestHeaders.Add("X-TXC-SIGNATURE", BitConverter.ToString(hash).ToLower().Replace("-", string.Empty));
            }

            var response = await _client.PostAsync(CoinbitServiceExtension.accountBalanceEndpoint, new StringContent(requestData));
            var s = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Response: {s}");
            var serializerOpt = new JsonSerializerOptions();
            serializerOpt.Converters.Add(new ResponseMessageConverter());
            return JsonSerializer.Deserialize<TResponse>(await response.Content.ReadAsStringAsync(), serializerOpt);
        }


    }
}
