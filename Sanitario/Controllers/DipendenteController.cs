using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    public class DipendenteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DipendenteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dipendentes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dipendenti.ToListAsync());
        }

        // GET: Dipendentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendenti
                .FirstOrDefaultAsync(m => m.IdDipendente == id);
            if (dipendente == null)
            {
                return NotFound();
            }

            return View(dipendente);
        }

        // GET: Dipendentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dipendentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Ruolo")] Dipendente dipendente)
        {
            if (ModelState.IsValid)
            {
                if (_context.Dipendenti.Any(d => d.Username == dipendente.Username))
                {
                    TempData["error"] = "Nome utente già in uso";
                    return View(dipendente);
                }
                _context.Add(dipendente);
                await _context.SaveChangesAsync();
                TempData["success"] = "Registrazione avvenuta con successo";
                return RedirectToAction("Index", "Login");
            }
            return View(dipendente);
        }

        // GET: Dipendentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendenti.FindAsync(id);
            if (dipendente == null)
            {
                return NotFound();
            }
            return View(dipendente);
        }

        // POST: Dipendentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDipendente,Username,Password,Ruolo")] Dipendente dipendente)
        {
            if (id != dipendente.IdDipendente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dipendente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DipendenteExists(dipendente.IdDipendente))
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
            return View(dipendente);
        }

        // GET: Dipendentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dipendente = await _context.Dipendenti
                .FirstOrDefaultAsync(m => m.IdDipendente == id);
            if (dipendente == null)
            {
                return NotFound();
            }

            return View(dipendente);
        }

        // POST: Dipendentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dipendente = await _context.Dipendenti.FindAsync(id);
            if (dipendente != null)
            {
                _context.Dipendenti.Remove(dipendente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DipendenteExists(int id)
        {
            return _context.Dipendenti.Any(e => e.IdDipendente == id);
        }

        public IActionResult BackOffice()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Farmacista")]
        public IActionResult Operazioni()
        {
            return View();
        }
    }
}
