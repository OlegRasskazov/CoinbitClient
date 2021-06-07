using System;

namespace Coinsbit.Client.Client.Models.Abstract
{
    public abstract class BaseRequestData
    {
        public string Request { get; set; }
        public string Nonce { get => Math.Truncate(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString(); }
    }
}
