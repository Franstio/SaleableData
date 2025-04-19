namespace SaleableData.Models
{
    public class SealableChartModel
    {
        public required string Date { get; set; }
        public required decimal Weight { get; set; }
        public override string ToString()
        {
            return Weight.ToString("{0.00}");
        }
    }
}
