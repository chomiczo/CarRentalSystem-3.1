using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRentalSystem.Controllers
{
    [Authorize]
    public class RentalModelsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public RentalModelsController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var rentedModels = await _context.RentalModel
                .Include(x => x.CarModel)
                .Where(r => r.CustomerId == user.Id)
                .ToListAsync();

            foreach (var rentalModel in rentedModels)
            {
                var carModel = rentalModel.CarModel;
                if (carModel != null)
                {
                    carModel.NumericRentPrice = carModel != null ? Convert.ToDecimal(carModel.RentPrice) : 0;
                }
            }

            return View(rentedModels);
        }



        // GET: RentalModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalModel = await _context.RentalModel
                .Include(x => x.CarModel) // Dodaj to, aby załadować CarModel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rentalModel == null)
            {
                return NotFound();
            }

            return View(rentalModel);
        }


        // GET: RentalModels/Create
        // GET: RentalModels/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var customer = await _context.CustomerModel.FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (customer == null || string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName) || string.IsNullOrEmpty(customer.Email))
            {
                TempData["ErrorMessage"] = "Proszę wypełnić wszystkie pola w formularzu klienta przed wynajęciem samochodu.";
                return RedirectToAction("Create", "CustomerModels");
            }

            var carModels = await _context.CarModel.ToListAsync();

            // Dodaj poniższy kod, aby sprawdzić, czy carModels są prawidłowo załadowane
            if (carModels == null)
            {
                // Jeśli carModels są null, możemy to zobaczyć w konsoli lub logach
                Console.WriteLine("carModels is null");
                // Możesz również dodać logowanie, jeśli korzystasz z logów
            }
            else
            {
                Console.WriteLine($"Number of carModels: {carModels.Count}");
            }

            var rentalModel = new RentalModel
            {
                CarModels = carModels,
                CustomerId = user.Id
                // ... inne właściwości
            };

            return View(rentalModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,RentalDate,ReturnDate")] RentalModel rentalModel)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var customer = await _context.CustomerModel.FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (customer == null || string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName) || string.IsNullOrEmpty(customer.Email))
            {
                TempData["ErrorMessage"] = "Proszę wypełnić wszystkie pola w formularzu klienta przed wynajęciem samochodu.";
                return RedirectToAction("Create", "CustomerModels");
            }

            // Set CustomerId to the current user's Id
            rentalModel.CustomerId = user.Id;

            // Pobierz odpowiednią instancję CarModel na podstawie CarId
            rentalModel.CarModel = await _context.CarModel.FindAsync(rentalModel.CarId);

            // Sprawdź, czy CarModel został znaleziony
            if (rentalModel.CarModel == null)
            {
                // Jeśli CarModel nie został znaleziony, zwróć błąd
                ModelState.AddModelError(string.Empty, "Nieprawidłowy model samochodu.");
                rentalModel.CarModels = await _context.CarModel.ToListAsync();
                return View("Create", rentalModel);
            }

            // Ustaw CarModelId na klucz główny powiązanego CarModel
            rentalModel.CarModelId = rentalModel.CarModel.Id;

            if (ModelState.IsValid)
            {
                _context.Add(rentalModel);
                await _context.SaveChangesAsync();

                // Przekaż cenę wynajmu do akcji Create kontrolera PaymentsController jako parametr
                var carModel = await _context.CarModel.FindAsync(rentalModel.CarId);
                if (carModel != null)
                {
                    return RedirectToAction("Create", "Payments", new { amount = carModel.RentPrice });
                }
            }

            // Jeśli ModelState.IsValid jest false, wróć do widoku z błędami
            rentalModel.CarModels = await _context.CarModel.ToListAsync();
            return View("Create", rentalModel);
        }




        // GET: RentalModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalModel = await _context.RentalModel.FindAsync(id);

            if (rentalModel == null)
            {
                return NotFound();
            }

            rentalModel.CarModels = await _context.CarModel.ToListAsync();

            return View(rentalModel);
        }


        // POST: RentalModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,CustomerId,RentalDate,ReturnDate")] RentalModel rentalModel)
        {
            if (id != rentalModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Pobierz odpowiednią instancję CarModel na podstawie CarId
                    rentalModel.CarModel = await _context.CarModel.FindAsync(rentalModel.CarId);

                    // Zaktualizuj CarModel na podstawie nowego CarId
                    rentalModel.CarModel = await _context.CarModel.FindAsync(rentalModel.CarId);

                    _context.Update(rentalModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalModelExists(rentalModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // Jeśli ModelState.IsValid jest false, wróć do widoku z błędami
            rentalModel.CarModels = await _context.CarModel.ToListAsync();
            return View(rentalModel);
        }


        // GET: RentalModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalModel = await _context.RentalModel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rentalModel == null)
            {
                return NotFound();
            }

            return View(rentalModel);
        }

        // POST: RentalModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RentalModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RentalModel' is null.");
            }

            var rentalModel = await _context.RentalModel.FindAsync(id);

            if (rentalModel != null)
            {
                _context.RentalModel.Remove(rentalModel);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool RentalModelExists(int id)
        {
            return (_context.RentalModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
