namespace Coinsbit.Client.Client.Models.AccountOrder
{
    public class RecordType
    {
        public double Time { get; set; }
        public string Fee { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public int Id { get; set; }
        public int DealOrderId { get; set; }
        public int Role { get; set; }
        public string Deal { get; set; }
    }
}
