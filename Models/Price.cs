using System.Data.SqlTypes;

namespace SecondTask.Models
{
    public class Price
    {
        public int? Id { get; set; }
        public decimal? Cost { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
