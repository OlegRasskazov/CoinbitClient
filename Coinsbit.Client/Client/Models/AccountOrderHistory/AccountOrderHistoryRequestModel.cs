using Coinsbit.Client.Client.Models.Abstract;

namespace Coinsbit.Client.Client.Models.AccountOrderHistory
{
    public class AccountOrderHistoryRequestModel: BaseRequestData
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 50;
    }
}
