using System;

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
        public decimal? NumericRentPrice => CarModel?.NumericRentPrice ?? 0;
        public DateTimeOffset? RentalDate { get; set; } // Zmiana typu na DateTimeOffset?
        public DateTimeOffset? ReturnDate { get; set; } // Zmiana typu na DateTimeOffset?
        public List<CarModel>? CarModels { get; set; }
    }
}
