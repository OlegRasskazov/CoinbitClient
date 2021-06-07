using Coinsbit.Client.Client.Models.Common;
using Coinsbit.Client.Client.Models.Converters;
using Coinsbit.Client.Client.Models.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinsbit.Client.Client.Models.AccountBalance
{
    public class AccountBalanceResponseModel
    {
        public bool Success { get; set; }
        [JsonConverter(typeof(ResponseMessageConverter))]
        public MessageType Message { get; set; }
        public IDictionary<CurrencyEnum, ResultType> Result { get; set; }
    }
}
