namespace SecondTask.Models
{
    public class OrderItem
    {
        public int? Id { get; set; }
        public int? PersonId { get; set; }
        public Person? Person { get; set; }
        public int? PriceId { get; set; }
        public Price? Price { get; set; }
        public int? ProductCount { get; set; }
        public DateTime? DateHandling { get; set; }
    }
}
