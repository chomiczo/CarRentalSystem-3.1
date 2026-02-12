using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Data;
using CarRentalSystem.Models;

namespace CarRentalSystem.Controllers
{
    public class DamageReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DamageReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DamageReports
        public async Task<IActionResult> Index(string searchString)
        {
            var reports = _context.DamageReport.Include(d => d.Car).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                reports = reports.Where(s => s.Description.Contains(searchString));
            }
            return View(await reports.ToListAsync());
        }

        // GET: DamageReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DamageReport == null)
            {
                return NotFound();
            }

            var damageReport = await _context.DamageReport
                .Include(d => d.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (damageReport == null)
            {
                return NotFound();
            }

            return View(damageReport);
        }

        // GET: DamageReports/Create
        public IActionResult Create()
        {
            ViewData["CarModelId"] = new SelectList(_context.CarModel, "Id",
            "Brand");
            return View();
        }

        // POST: DamageReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
        Create([Bind("Id,Description,ReportDate,CarModelId")]
CarRentalSystem.Models.DamageReport damageReport)
        {
            ModelState.Remove("Car");
            if (ModelState.IsValid)
            {
                _context.Add(damageReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModel, "Id",
            "Brand", damageReport.CarModelId);
            return View(damageReport);
        }

        // GET: DamageReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DamageReport == null)
            {
                return NotFound();
            }
            var damageReport = await _context.DamageReport.FindAsync(id);
            if (damageReport == null)
            {
                return NotFound();
            }
            // Kluczowe: przekazujemy "Brand" do listy rozwijanej
            ViewData["CarModelId"] = new SelectList(_context.CarModel, "Id",
            "Brand", damageReport.CarModelId);
            return View(damageReport);
        }

        // POST: DamageReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
        [Bind("Id,Description,ReportDate,CarModelId")]
CarRentalSystem.Models.DamageReport damageReport)
        {
            if (id != damageReport.Id)
            {
                return NotFound();
            }
            // Kluczowe: ignorujemy obiekt nawigacyjny, by walidacja przeszła
            ModelState.Remove("Car");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(damageReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DamageReportExists(damageReport.Id))
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
            // Jeśli walidacja się nie uda, ponownie ładujemy listę marek (Brand)
            ViewData["CarModelId"] = new SelectList(_context.CarModel, "Id",
            "Brand", damageReport.CarModelId);
            return View(damageReport);
        }

        // GET: DamageReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DamageReport == null)
            {
                return NotFound();
            }

            var damageReport = await _context.DamageReport
                .Include(d => d.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (damageReport == null)
            {
                return NotFound();
            }

            return View(damageReport);
        }

        // POST: DamageReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DamageReport == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DamageReport'  is null.");
            }
            var damageReport = await _context.DamageReport.FindAsync(id);
            if (damageReport != null)
            {
                _context.DamageReport.Remove(damageReport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DamageReportExists(int id)
        {
          return (_context.DamageReport?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
