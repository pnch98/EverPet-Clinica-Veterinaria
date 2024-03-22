using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    [Authorize(Roles = "Admin, Farmacista")]
    public class CassettoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CassettoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cassetto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cassetti.Include(c => c.Armadietto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cassetto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cassetto = await _context.Cassetti
                .Include(c => c.Armadietto)
                .FirstOrDefaultAsync(m => m.IdCassetto == id);
            if (cassetto == null)
            {
                return NotFound();
            }

            return View(cassetto);
        }

        // GET: Cassetto/Create
        public IActionResult Create()
        {
            ViewData["IdArmadietto"] = new SelectList(_context.Armadietti, "IdArmadietto", "IdArmadietto");
            return View();
        }

        // POST: Cassetto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArmadietto,NumeroCassetto")] Cassetto cassetto)
        {
            ModelState.Remove("Armadietto");
            ModelState.Remove("Medicinali");
            if (ModelState.IsValid)
            {
                _context.Add(cassetto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdArmadietto"] = new SelectList(_context.Armadietti, "IdArmadietto", "IdArmadietto", cassetto.IdArmadietto);
            return View(cassetto);
        }

        // GET: Cassetto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cassetto = await _context.Cassetti.FindAsync(id);
            if (cassetto == null)
            {
                return NotFound();
            }
            ViewData["IdArmadietto"] = new SelectList(_context.Armadietti, "IdArmadietto", "IdArmadietto", cassetto.IdArmadietto);
            return View(cassetto);
        }

        // POST: Cassetto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCassetto,IdArmadietto,NumeroCassetto")] Cassetto cassetto)
        {
            if (id != cassetto.IdCassetto)
            {
                return NotFound();
            }

            ModelState.Remove("Armadietto");
            ModelState.Remove("Medicinali");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cassetto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CassettoExists(cassetto.IdCassetto))
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
            ViewData["IdArmadietto"] = new SelectList(_context.Armadietti, "IdArmadietto", "IdArmadietto", cassetto.IdArmadietto);
            return View(cassetto);
        }

        // GET: Cassetto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cassetto = await _context.Cassetti
                .Include(c => c.Armadietto)
                .FirstOrDefaultAsync(m => m.IdCassetto == id);
            if (cassetto == null)
            {
                return NotFound();
            }

            return View(cassetto);
        }

        // POST: Cassetto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cassetto = await _context.Cassetti.FindAsync(id);
            if (cassetto != null)
            {
                _context.Cassetti.Remove(cassetto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CassettoExists(int id)
        {
            return _context.Cassetti.Any(e => e.IdCassetto == id);
        }
    }
}
