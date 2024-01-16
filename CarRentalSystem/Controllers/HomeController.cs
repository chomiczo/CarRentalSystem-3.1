using CarRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var photos = GetPhotosFromDatabase();
            return View(photos);
        }
        private List<PhotoModel> GetPhotosFromDatabase()
        {

            return new List<PhotoModel>
        {
            new PhotoModel { Id = 1, ImagePath = "/images/photo1.jpg" },
            new PhotoModel { Id = 2, ImagePath = "/images/photo2.jpg" },
            new PhotoModel { Id = 3, ImagePath = "/images/g1.jpg" },
            new PhotoModel { Id = 4, ImagePath = "/images/g2.jpg" },
            new PhotoModel { Id = 5, ImagePath = "/images/g3.jpg" },
            new PhotoModel { Id = 6, ImagePath = "/images/g4.jpg" },
            new PhotoModel { Id = 7, ImagePath = "/images/g5.jpg" },
            new PhotoModel { Id = 8, ImagePath = "/images/g6.jpg" },
            new PhotoModel { Id = 9, ImagePath = "/images/g7.jpg" },
            new PhotoModel { Id = 10, ImagePath = "/images/g8.jpg" },
            new PhotoModel { Id = 11, ImagePath = "/images/g9.jpg" }
            // Dodaj więcej zdjęć
        };
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult omnie()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}