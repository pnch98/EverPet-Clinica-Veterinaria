using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    [Authorize(Roles = "Admin, Farmacista")]
    public class DettagliVenditaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DettagliVenditaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DettagliVendita
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DettagliVendite.Include(d => d.Prodotto).Include(d => d.Vendita);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DettagliVendita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dettagliVendita = await _context.DettagliVendite
                .Include(d => d.Prodotto)
                .Include(d => d.Vendita)
                .FirstOrDefaultAsync(m => m.IdDettagliVendita == id);
            if (dettagliVendita == null)
            {
                return NotFound();
            }

            return View(dettagliVendita);
        }

        // GET: DettagliVendita/Create
        public IActionResult Create()
        {
            ViewData["IdProdotto"] = new SelectList(_context.Prodotti, "IdProdotto", "Descrizione");
            ViewData["IdVendita"] = new SelectList(_context.Vendite, "IdVendita", "IdVendita");
            return View();
        }

        // POST: DettagliVendita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVendita,IdProdotto")] DettagliVendita dettagliVendita)
        {
            ModelState.Remove("Vendita");
            ModelState.Remove("Prodotto");
            if (ModelState.IsValid)
            {
                _context.Add(dettagliVendita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProdotto"] = new SelectList(_context.Prodotti, "IdProdotto", "Descrizione", dettagliVendita.IdProdotto);
            ViewData["IdVendita"] = new SelectList(_context.Vendite, "IdVendita", "IdVendita", dettagliVendita.IdVendita);
            return View(dettagliVendita);
        }

        // GET: DettagliVendita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dettagliVendita = await _context.DettagliVendite.FindAsync(id);
            if (dettagliVendita == null)
            {
                return NotFound();
            }
            ViewData["IdProdotto"] = new SelectList(_context.Prodotti, "IdProdotto", "Descrizione", dettagliVendita.IdProdotto);
            ViewData["IdVendita"] = new SelectList(_context.Vendite, "IdVendita", "IdVendita", dettagliVendita.IdVendita);
            return View(dettagliVendita);
        }

        // POST: DettagliVendita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDettagliVendita,IdVendita,IdProdotto")] DettagliVendita dettagliVendita)
        {
            if (id != dettagliVendita.IdDettagliVendita)
            {
                return NotFound();
            }

            ModelState.Remove("Vendita");
            ModelState.Remove("Prodotto");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dettagliVendita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DettagliVenditaExists(dettagliVendita.IdDettagliVendita))
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
            ViewData["IdProdotto"] = new SelectList(_context.Prodotti, "IdProdotto", "Descrizione", dettagliVendita.IdProdotto);
            ViewData["IdVendita"] = new SelectList(_context.Vendite, "IdVendita", "IdVendita", dettagliVendita.IdVendita);
            return View(dettagliVendita);
        }

        // GET: DettagliVendita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dettagliVendita = await _context.DettagliVendite
                .Include(d => d.Prodotto)
                .Include(d => d.Vendita)
                .FirstOrDefaultAsync(m => m.IdDettagliVendita == id);
            if (dettagliVendita == null)
            {
                return NotFound();
            }

            return View(dettagliVendita);
        }

        // POST: DettagliVendita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dettagliVendita = await _context.DettagliVendite.FindAsync(id);
            if (dettagliVendita != null)
            {
                _context.DettagliVendite.Remove(dettagliVendita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DettagliVenditaExists(int id)
        {
            return _context.DettagliVendite.Any(e => e.IdDettagliVendita == id);
        }
    }
}
