namespace CarRentalSystem.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
    }
}
