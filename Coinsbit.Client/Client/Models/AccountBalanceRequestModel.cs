using Coinsbit.Client.Client.Models.Enums;
using System.Text.Json.Serialization;

namespace Coinsbit.Client.Client.Models
{
    public class AccountBalanceRequestModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CurrencyEnum Currency { get; set; } = CurrencyEnum.ETH;
    }
}
