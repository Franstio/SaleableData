namespace SaleableData.Models
{
    public class TransactionModel
    {
        public DateTime RecordDate { get; set; }
        public required string Id { get; set; }
        public required string ToBin { get; set; }
        public required string Status { get; set; }
        public required string FromContainer { get; set; }
        public required string Type { get; set; }
        public required string Scales { get; set; }
        public required decimal Weight { get; set; }
        public required int Group_Id { get; set; }
    }
}
