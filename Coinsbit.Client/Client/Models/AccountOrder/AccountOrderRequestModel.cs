using Coinsbit.Client.Client.Models.Abstract;

namespace Coinsbit.Client.Client.Models.AccountOrder
{
    public class AccountOrderRequestModel: BaseRequestData
    {
        public int OrderId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
