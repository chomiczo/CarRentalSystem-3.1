using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalSystem.Controllers
{
    public class CarModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarModels
        public async Task<IActionResult> Index()
        {
            return _context.CarModel != null ?
                        View(await _context.CarModel.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.CarModel'  is null.");
        }

        // GET: CarModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarModel == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: CarModels/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,Year,RentPrice")] CarModel carModel)
        {
            // Sprawdź, czy użytkownik ma wymagane uprawnienia
            if (!User.IsInRole("Administrator"))
            {
                TempData["ErrorMessage"] = "Brak uprawnień do dodawania nowego modelu samochodu.";
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: CarModels/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            // Sprawdź, czy użytkownik jest administratorem
            if (!User.IsInRole("Administrator"))
            {
                // Użytkownik nie jest administratorem, więc zwróć widok informacyjny
                return View("AccessDenied");
            }
            if (id == null || _context.CarModel == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Year,RentPrice")] CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.Id))
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
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            // Sprawdź, czy użytkownik jest administratorem
            if (!User.IsInRole("Administrator"))
            {
                // Użytkownik nie jest administratorem, więc zwróć widok informacyjny
                return View("AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (carModel == null)
            {
                return NotFound();
            }

            // Sprawdź, czy istnieją powiązane rekordy w RentalModel
            var hasRelatedRentals = await _context.RentalModel.AnyAsync(r => r.CarModelId == id);

            if (hasRelatedRentals)
            {
                // Jeśli istnieją powiązane rekordy, możesz obsłużyć ten przypadek
                TempData["ErrorMessage"] = "Nie można usunąć tego modelu, ponieważ jest już on wynajmowany przez jakiegoś użytkownika.";
                return RedirectToAction(nameof(Index));
            }

            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.CarModel == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.CarModel' is null.");
                }

                var carModel = await _context.CarModel.FindAsync(id);

                if (carModel == null)
                {
                    // Jeśli nie znaleziono rekordu do usunięcia, możesz obsłużyć ten przypadek
                    return NotFound();
                }

                // Sprawdź, czy istnieją powiązane rekordy w RentalModel
                var hasRelatedRentals = await _context.RentalModel.AnyAsync(r => r.CarModelId == id);

                if (hasRelatedRentals)
                {
                    // Jeśli istnieją powiązane rekordy, możesz obsłużyć ten przypadek
                    TempData["ErrorMessage"] = "Nie można usunąć tego modelu, ponieważ jest już on wynajmowany.";
                    return RedirectToAction(nameof(Index));
                }

                _context.CarModel.Remove(carModel);
                await _context.SaveChangesAsync();

                // Przekieruj użytkownika do akcji Index po pomyślnym usunięciu
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Obsłuż wyjątek ogólny (Exception) i zwróć odpowiedni komunikat lub przekieruj użytkownika do strony z komunikatem
                return Problem("Wystąpił błąd podczas usuwania rekordu.", null, 500);
            }
        }
        private bool CarModelExists(int id)
        {
            return (_context.CarModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
