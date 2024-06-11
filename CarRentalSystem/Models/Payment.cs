using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Payment
    {
    
        public int Id { get; set; }
        public int RentalId { get; set; }

        [Display(Name = "Metoda płatności")]
        public string? PaymentMethod { get; set; }


        [Display(Name = "Kwota")]
        public string? Amount { get; set; }
    }
}
