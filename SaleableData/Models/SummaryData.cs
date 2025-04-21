namespace SaleableData.Models
{
    public class SummaryData
    {
        public SummaryData(List<TransactionModel> transactions)
        {
            decimal[] data = transactions.OrderBy(x=>x.Weight).Select(x => x.Weight).ToArray();
            Q2 = Median(data);
            int MedianIndex = data.Length % 2 == 0 ? data.Length / 2 : (data.Length + 1) / 2;
            decimal[] lower = data.Take(MedianIndex).ToArray();
            decimal[] upper = data.Skip(MedianIndex).ToArray();
            Q1 = Median(lower);
            Q3=  Median(upper);
            MaxValue = transactions.MaxBy(x=>x.Weight)!;
            MinValue = transactions.MinBy(x=>x.Weight)!;

        }
        decimal Median( decimal[] data)
        {
            return (data.Length % 2 == 0) ? ((data[(data.Length / 2)-1] + data[((data.Length / 2) + 1)-1]) / 2) : data[((data.Length + 1) / 2) - 1];
        }
        public  decimal Q1 { get;set; }
        public decimal Q2 { get; set; }
        public decimal Q3 { get; set; }
        public TransactionModel MaxValue { get; set; }  
        public TransactionModel MinValue { get; set; }
    }
}
