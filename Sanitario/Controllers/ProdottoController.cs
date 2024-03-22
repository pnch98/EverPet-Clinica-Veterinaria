using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    [Authorize(Roles = "Admin, Farmacista")]
    public class ProdottoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdottoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prodotto
        public async Task<IActionResult> Index()
        {
            var prodotti = await _context.Prodotti
                            .Include(p => p.Cassetto)
                            .ThenInclude(c => c.Armadietto)
                            .ToListAsync();

            return View(prodotti);
        }

        // GET: Prodotto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti
                .FirstOrDefaultAsync(m => m.IdProdotto == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // GET: Prodotto/Create
        public IActionResult Create()
        {
            ViewBag.Cassetti = _context.Cassetti.Select(c => new SelectListItem
            {
                Value = c.IdCassetto.ToString(),
                Text = $"Cassetto: {c.NumeroCassetto} - Armadietto: {c.Armadietto.NumeroArmadietto}"
            }).ToList();
            return View();
        }

        // POST: Prodotto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descrizione,Prezzo, TipoProdotto, IdCassetto")] Prodotto prodotto)
        {
            ModelState.Remove("DettagliVendite");
            ModelState.Remove("CurePrescritte");
            ModelState.Remove("Cassetto");
            if (ModelState.IsValid)
            {
                _context.Add(prodotto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Cassetti = new SelectList(_context.Cassetti, "IdCassetto", "NomeCassetto");
            return View(prodotto);
        }

        // GET: Prodotto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }

        // POST: Prodotto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProdotto,Nome,Descrizione,Prezzo")] Prodotto prodotto)
        {
            if (id != prodotto.IdProdotto)
            {
                return NotFound();
            }

            ModelState.Remove("DettagliVendite");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodotto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottoExists(prodotto.IdProdotto))
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
            return View(prodotto);
        }

        // GET: Prodotto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti
                .FirstOrDefaultAsync(m => m.IdProdotto == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // POST: Prodotto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto != null)
            {
                _context.Prodotti.Remove(prodotto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdottoExists(int id)
        {
            return _context.Prodotti.Any(e => e.IdProdotto == id);
        }

        // Questo l'ho messo per testare da postman
        [AllowAnonymous]
        public async Task<IActionResult> SearchMedicinale(string medicinale)
        {

            var prodotto = await _context.Prodotti
                .Where(p => p.TipoProdotto == "Medicinale" && p.Nome.Contains(medicinale))
                .Select(p => new
                {
                    p.IdProdotto,
                    p.Nome,
                    p.Descrizione,
                    p.Prezzo,
                    p.TipoProdotto,
                    Armadietto = new
                    {
                        numeroArmadietto = p.Cassetto.Armadietto.NumeroArmadietto,
                        numeroCassetto = p.Cassetto.NumeroCassetto
                    }
                })
                .ToListAsync();


            if (prodotto == null)
            {
                return NotFound();
            }
            return Json(prodotto);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetMedicinaleByDate(DateOnly data)
        {
            var listMedicinali = await _context.Prodotti
                .Where(p => p.TipoProdotto == "Medicinale" && p.DettagliVendite.Any(d => DateOnly.FromDateTime(d.Vendita.DataVendita.Date) == data))
                .Select(p => new
                {
                    p.IdProdotto,
                    p.Nome,
                    p.Descrizione,
                    p.Prezzo,
                    p.TipoProdotto
                })
                .ToListAsync();
            return Json(listMedicinali);

        }

        [AllowAnonymous]
        public async Task<IActionResult> GetMedicinaliByCF(string codiceFiscale)
        {
            var listMedicinali = await _context.Prodotti
                .Where(p => p.TipoProdotto == "Medicinale" && p.DettagliVendite.Any(d => d.Vendita.Cliente.CodiceFiscale == codiceFiscale))
                .Select(p => new
                {
                    p.IdProdotto,
                    p.Nome,
                    p.Descrizione,
                    p.Prezzo,
                    p.TipoProdotto
                })
                .ToListAsync();
            return Json(listMedicinali);
        }
    }
}
