namespace SecondTask.Models
{
    public class Person
    {
        public int? Id { get; set; }
        public int? RegionId { get; set; }
        public Region? Region { get; set; }
        public string? PersonName { get; set; }
    }
}
