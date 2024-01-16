namespace CarRentalSystem.Models
{
    public class RentalModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string? CustomerId { get; set; }
        public CustomerModel? Customer { get; set; }
        public CarModel? CarModel { get; set; }
        public int CarModelId { get; set; }
        public string Brand => CarModel?.Brand ?? "Unknown";
        public string Model => CarModel?.Model ?? "Unknown";
        public DateTime? RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public List<CarModel>? CarModels { get; set; }

    }
}
