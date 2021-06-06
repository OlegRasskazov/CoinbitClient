using Coinsbit.Client.Client.Models.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinsbit.Client.Client.Models
{
    public class AccountBalanceResponseModel
    {
        public bool Success { get; set; }
        [JsonConverter(typeof(ResponseMessageConverter))]
        public MessageType Message { get; set; }
        public ResultType Result { get; set; }
    }

    public class ResultType
    {
        public string Available { get; set; }
        public string Freeze { get; set; }
        public List<object> SomeResult { get; set; }
    }
    public class MessageType
    {
        public string Message { get; set; }
        public List<List<string>> Messages { get; set; }
    }
}
