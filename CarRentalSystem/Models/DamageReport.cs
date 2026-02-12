namespace CarRentalSystem.Models
{
    public class DamageReport
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? ReportDate { get; set; }
        public int CarModelId { get; set; } //klucz obcy
        public CarModel? Car { get; set; }
    }
}
