namespace SecondTask.Models
{
    public class TopRegionsProducts
    {
        public int? Id { get; set; }
        public string? RegionName { get; set; }
        public string? ProductName { get; set;}
        public int? TotalCount { get; set; }
        public int? TotalBuyers { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
