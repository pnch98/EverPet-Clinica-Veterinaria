using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    [Authorize(Roles = "Admin, Farmacista")]
    public class ArmadiettoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArmadiettoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Armadietto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Armadietti.ToListAsync());
        }

        // GET: Armadietto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armadietto = await _context.Armadietti
                .FirstOrDefaultAsync(m => m.IdArmadietto == id);
            if (armadietto == null)
            {
                return NotFound();
            }

            return View(armadietto);
        }

        // GET: Armadietto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Armadietto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroArmadietto")] Armadietto armadietto)
        {
            ModelState.Remove("Cassetti");
            if (ModelState.IsValid)
            {
                _context.Add(armadietto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(armadietto);
        }

        // GET: Armadietto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armadietto = await _context.Armadietti.FindAsync(id);
            if (armadietto == null)
            {
                return NotFound();
            }
            return View(armadietto);
        }

        // POST: Armadietto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdArmadietto,NumeroArmadietto")] Armadietto armadietto)
        {
            if (id != armadietto.IdArmadietto)
            {
                return NotFound();
            }

            ModelState.Remove("Cassetti");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armadietto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmadiettoExists(armadietto.IdArmadietto))
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
            return View(armadietto);
        }

        // GET: Armadietto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armadietto = await _context.Armadietti
                .FirstOrDefaultAsync(m => m.IdArmadietto == id);
            if (armadietto == null)
            {
                return NotFound();
            }

            return View(armadietto);
        }

        // POST: Armadietto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armadietto = await _context.Armadietti.FindAsync(id);
            if (armadietto != null)
            {
                _context.Armadietti.Remove(armadietto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmadiettoExists(int id)
        {
            return _context.Armadietti.Any(e => e.IdArmadietto == id);
        }
    }
}
