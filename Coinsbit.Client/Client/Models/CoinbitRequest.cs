using Coinsbit.Client.Client.Models.Abstract;
using System;

namespace Coinsbit.Client.Client.Models
{
    public class CoinbitRequest<T> where T : BaseRequestData
    {
        public CoinbitRequest(BaseRequestData data) => Request = (T)data;

        public string Nonce { get => Math.Truncate(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString(); }
        public T Request { get; set; }
    }
}
