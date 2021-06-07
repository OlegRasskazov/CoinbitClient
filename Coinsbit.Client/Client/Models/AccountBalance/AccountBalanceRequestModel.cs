using Coinsbit.Client.Client.Models.Abstract;
using Coinsbit.Client.Client.Models.Enums;
using System.Text.Json.Serialization;

namespace Coinsbit.Client.Client.Models.AccountBalance
{
    public class AccountBalanceRequestModel : BaseRequestData
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CurrencyEnum Currency { get; set; } = CurrencyEnum.HMR;
    }
}
