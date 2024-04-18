using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Metoda płatności")]
        public string? PaymentMethod { get; set; }

        [Required]
        [Display(Name = "Kwota")]
        public string? Amount { get; set; }
    }
}
