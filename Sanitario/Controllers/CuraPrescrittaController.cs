using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    [Authorize(Roles = "Admin, Farmacista, Veterinario")]
    public class CuraPrescrittaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuraPrescrittaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CuraPrescritta
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CurePrescritte.Include(c => c.Visita);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CuraPrescritta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curaPrescritta = await _context.CurePrescritte
                .Include(c => c.Visita)
                .FirstOrDefaultAsync(m => m.IdCuraPrescritta == id);
            if (curaPrescritta == null)
            {
                return NotFound();
            }

            return View(curaPrescritta);
        }

        // GET: CuraPrescritta/Create
        public IActionResult Create()
        {
            ViewData["IdVisita"] = new SelectList(_context.Visite, "Id", "Esame");
            return View();
        }

        // POST: CuraPrescritta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVisita,IdProdotto")] CuraPrescritta curaPrescritta)
        {
            ModelState.Remove("Visita");
            ModelState.Remove("Medicinali");
            if (ModelState.IsValid)
            {
                _context.Add(curaPrescritta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVisita"] = new SelectList(_context.Visite, "Id", "Esame", curaPrescritta.IdVisita);
            return View(curaPrescritta);
        }

        // GET: CuraPrescritta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curaPrescritta = await _context.CurePrescritte.FindAsync(id);
            if (curaPrescritta == null)
            {
                return NotFound();
            }
            ViewData["IdVisita"] = new SelectList(_context.Visite, "Id", "Esame", curaPrescritta.IdVisita);
            return View(curaPrescritta);
        }

        // POST: CuraPrescritta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCuraPrescritta,IdVisita,IdProdotto")] CuraPrescritta curaPrescritta)
        {
            if (id != curaPrescritta.IdCuraPrescritta)
            {
                return NotFound();
            }

            ModelState.Remove("Visita");
            ModelState.Remove("Medicinali");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curaPrescritta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuraPrescrittaExists(curaPrescritta.IdCuraPrescritta))
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
            ViewData["IdVisita"] = new SelectList(_context.Visite, "Id", "Esame", curaPrescritta.IdVisita);
            return View(curaPrescritta);
        }

        // GET: CuraPrescritta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curaPrescritta = await _context.CurePrescritte
                .Include(c => c.Visita)
                .FirstOrDefaultAsync(m => m.IdCuraPrescritta == id);
            if (curaPrescritta == null)
            {
                return NotFound();
            }

            return View(curaPrescritta);
        }

        // POST: CuraPrescritta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curaPrescritta = await _context.CurePrescritte.FindAsync(id);
            if (curaPrescritta != null)
            {
                _context.CurePrescritte.Remove(curaPrescritta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuraPrescrittaExists(int id)
        {
            return _context.CurePrescritte.Any(e => e.IdCuraPrescritta == id);
        }
    }
}
