using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    [Authorize(Roles = "Admin, Veterinario")]
    public class VisitaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visita
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Visite.Include(v => v.Animale).ThenInclude(a => a.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Visita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visite
                .Include(v => v.Animale)
                .ThenInclude(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // GET: Visita/Create
        public IActionResult Create()
        {
            ViewData["IdAnimale"] = new SelectList(_context.Animali.Include(a => a.Cliente), "IdAnimale", "NomeCompleto");
            ViewBag.ListaMedicinali = new MultiSelectList(_context.Prodotti.Where(p => p.TipoProdotto == "Medicinale"), "IdProdotto", "Nome");
            return View();
        }



        // POST: Visita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAnimale,DataVisita,Esame")] Visita visita, List<int> medicinaliSelezionati)
        {
            ModelState.Remove("CurePrescritte");
            ModelState.Remove("Animale");
            if (ModelState.IsValid)
            {
                visita.CurePrescritte = new List<CuraPrescritta>();
                _context.Add(visita);
                await _context.SaveChangesAsync();
                foreach (var idMedicinale in medicinaliSelezionati)
                {
                    var curaPrescritta = new CuraPrescritta { IdProdotto = idMedicinale, IdVisita = visita.Id };
                    _context.Add(curaPrescritta);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAnimale"] = new SelectList(_context.Animali.Include(a => a.Cliente), "IdAnimale", "NomeCompleto", visita.IdAnimale);
            ViewBag.ListaMedicinali = new MultiSelectList(_context.Prodotti.Where(p => p.TipoProdotto == "Medicinale"), "IdProdotto", "Nome");
            return View(visita);
        }

        // GET: Visita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visite.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }
            ViewData["IdAnimale"] = new SelectList(_context.Animali.Include(a => a.Cliente), "IdAnimale", "NomeCompleto", visita.IdAnimale);
            return View(visita);
        }

        // POST: Visita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAnimale,DataVisita,Esame")] Visita visita)
        {
            if (id != visita.Id)
            {
                return NotFound();
            }

            var checkIdAnimale = _context.Animali.AsNoTracking().FirstOrDefault(a => a.IdAnimale == id);
            if (checkIdAnimale.IdAnimale != visita.IdAnimale)
            {
                TempData["error"] = "Non puoi modificare l'ID della visita";

                ViewData["IdAnimale"] = new SelectList(_context.Animali.Include(a => a.Cliente), "IdAnimale", "NomeCompleto", visita.IdAnimale);
                return View(visita);
            }

            ModelState.Remove("CurePrescritte");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitaExists(visita.Id))
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
            ViewData["IdAnimale"] = new SelectList(_context.Animali, "IdAnimale", "ColoreMantello", visita.IdAnimale);
            return View(visita);
        }

        // GET: Visita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _context.Visite
                .Include(v => v.Animale)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // POST: Visita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visita = await _context.Visite.FindAsync(id);
            if (visita != null)
            {
                _context.Visite.Remove(visita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitaExists(int id)
        {
            return _context.Visite.Any(e => e.Id == id);
        }
    }
}
