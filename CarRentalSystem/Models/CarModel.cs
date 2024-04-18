using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace CarRentalSystem.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?|\d+(\,\d{1,2})?$", ErrorMessage = "Cena wynajmu (PLN) musi być liczbą z dwoma miejscami po przecinku.")]

        public double RentPrice { get; set; }
        public decimal NumericRentPrice { get; set; } // pole jako liczba
        public int? CustomerId { get; set; }
        public virtual CustomerModel? Customer { get; set; }

        public string BrandModel()
        {
            return $"{Brand ?? string.Empty} {Model ?? string.Empty}";
        }
    }
}
