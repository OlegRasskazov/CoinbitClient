using Coinsbit.Client.Client.Models.Common;
using Coinsbit.Client.Client.Models.Converters;
using System.Text.Json.Serialization;

namespace Coinsbit.Client.Client.Models.AccountOrder
{
    public class AccountOrderResponseModel
    {
        public bool Success { get; set; }
        [JsonConverter(typeof(ResponseMessageConverter))]
        public MessageType Message { get; set; }
        public ResultType Result { get; set; }
    }
}
