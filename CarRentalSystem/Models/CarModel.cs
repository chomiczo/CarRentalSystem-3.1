using Microsoft.AspNetCore.Identity;
namespace CarRentalSystem.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }

        public int? CustomerId { get; set; }
        public virtual CustomerModel? Customer { get; set; }

        public string BrandModel()
        {
            return $"{Brand ?? string.Empty} {Model ?? string.Empty}";
        }
    }
}
