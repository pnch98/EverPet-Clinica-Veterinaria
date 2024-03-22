using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sanitario.Data;
using Sanitario.Models;

namespace Sanitario.Controllers
{
    [Authorize(Roles = "Admin, Farmacista")]
    public class VenditaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenditaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vendita
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vendite.Include(v => v.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vendita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendita = await _context.Vendite
                .Include(v => v.Cliente)
                .Include(v => v.DettagliVendite)
                .ThenInclude(dv => dv.Prodotto)
                .FirstOrDefaultAsync(m => m.IdVendita == id);
            if (vendita == null)
            {
                return NotFound();
            }

            return View(vendita);
        }

        // GET: Vendita/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "NomeCompleto");
            ViewData["Products"] = new SelectList(_context.Prodotti.Where(p => p.TipoProdotto == "Comune"), "IdProdotto", "NomeCompleto");
            var session = HttpContext.Session.GetString("productList");
            if (session != null)
            {
                var productList = JsonConvert.DeserializeObject<List<Prodotto>>(session);
                if (productList.Count > 0)
                {
                    ViewData["Session"] = productList;
                }
                else
                {
                    HttpContext.Session.Remove("productList");
                }
            }



            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetVisite(int? idCliente)
        {
            try
            {
                if (idCliente == null)
                {
                    return BadRequest("Id cliente non fornito");
                }

                var visite = await _context.Visite
                            .Include(v => v.Animale)
                            .Where(v => v.Animale.IdCliente == idCliente && v.IsArchiviato == false)
                            .ToListAsync();

                // Costruisci una lista di oggetti con le informazioni necessarie
                var visitaData = visite.Select(v => new
                {
                    id = v.Id,
                    data = v.DataVisita,
                    esame = v.Esame,
                    nomeAnimale = v.Animale.Nome  // Aggiungi il nome dell'animale alla risposta
                });

                return Json(new { listaVisite = visitaData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCure(int? idVisita)
        {
            try
            {
                if (idVisita == null)
                {
                    return BadRequest("Id visita non fornito");
                }

                TempData["IdVisita"] = idVisita;

                var cure = await _context.CurePrescritte
                            .Include(cp => cp.Prodotto)
                            .Where(cp => cp.IdVisita == idVisita)
                            .ToListAsync();

                // Costruisci una lista di oggetti con le informazioni necessarie
                var cureData = cure.Select(cp => new
                {
                    id = cp.IdCuraPrescritta,
                    prodotto = new
                    {
                        idProdotto = cp.IdProdotto,
                        nome = cp.Prodotto.Nome,
                        prezzo = cp.Prodotto.Prezzo
                    }
                });

                return Json(new { listaCure = cureData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpPost]
        public IActionResult AddProductToSession(int idProdotto)
        {
            var prodotto = _context.Prodotti.Find(idProdotto);
            if (prodotto == null)
            {
                return NotFound();
            }

            var cartSession = HttpContext.Session.GetString("productList");
            List<Prodotto> productList;

            if (string.IsNullOrEmpty(cartSession))
            {
                productList = new List<Prodotto>();
            }
            else
            {
                productList = JsonConvert.DeserializeObject<List<Prodotto>>(cartSession);
            }

            productList.Add(prodotto);
            HttpContext.Session.SetString("productList", JsonConvert.SerializeObject(productList));

            return Json(new { list = productList });
        }

        [HttpPost]
        public IActionResult RemoveProductFromSession(int idProdotto)
        {
            var product = _context.Prodotti.Find(idProdotto);
            if (product == null)
            {
                return NotFound();
            }

            var cartSession = HttpContext.Session.GetString("productList");
            List<Prodotto> productList;

            if (string.IsNullOrEmpty(cartSession))
            {
                return StatusCode(500, "Errore interno del server");
            }
            else
            {
                productList = JsonConvert.DeserializeObject<List<Prodotto>>(cartSession);
            }

            productList.Remove(productList.FirstOrDefault(p => p.IdProdotto == idProdotto)); // Rimuovi il prodotto dalla lista

            HttpContext.Session.SetString("productList", JsonConvert.SerializeObject(productList));

            return Json(new { list = productList }); // Restituisci la lista aggiornata senza il prodotto rimosso
        }


        // POST: Vendita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,DataVendita")] Vendita vendita, int idVisita)
        {
            ModelState.Remove("DettagliVendite");
            ModelState.Remove("Cliente");
            if (idVisita == 0)
            {
                ModelState.Remove("idVisita");
            }

            if (ModelState.IsValid)
            {
                double prezzoTotale = 0;
                var curePrescritte = new List<CuraPrescritta>();

                if (idVisita != 0)
                {
                    curePrescritte = await _context.CurePrescritte
                    .Include(cp => cp.Prodotto)
                    .Where(cp => cp.IdVisita == idVisita)
                    .ToListAsync();

                    foreach (var cura in curePrescritte)
                    {
                        prezzoTotale += cura.Prodotto.Prezzo;
                    }
                }

                var productListSession = HttpContext.Session.GetString("productList");
                List<Prodotto> productList = new List<Prodotto>();
                if (productListSession != null)
                {
                    productList = JsonConvert.DeserializeObject<List<Prodotto>>(productListSession);
                    foreach (var product in productList)
                    {
                        prezzoTotale += product.Prezzo;
                    }
                }

                vendita.PrezzoTotale = prezzoTotale;
                _context.Add(vendita);
                await _context.SaveChangesAsync();

                if (productListSession != null)
                {
                    foreach (var product in productList)
                    {
                        var dettaglioVendita = new DettagliVendita
                        {
                            IdVendita = vendita.IdVendita,
                            IdProdotto = product.IdProdotto,
                        };
                        _context.DettagliVendite.Add(dettaglioVendita);
                    }
                }

                if (curePrescritte.Count > 0)
                {
                    foreach (var cura in curePrescritte)
                    {
                        var dettaglioVendita = new DettagliVendita
                        {
                            IdVendita = vendita.IdVendita,
                            IdProdotto = cura.IdProdotto,
                        };
                        _context.DettagliVendite.Add(dettaglioVendita);
                    }

                    var visita = await _context.Visite.FindAsync(idVisita);
                    visita.IsArchiviato = true;

                    _context.Visite.Update(visita);
                }


                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("productList");
                return RedirectToAction(nameof(Index));
            }
            var session = HttpContext.Session.GetString("productList");
            if (session != null)
            {
                var productList = JsonConvert.DeserializeObject<List<Prodotto>>(session);
                ViewData["Session"] = productList;
            }
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CodiceFiscale", vendita.IdCliente);
            ViewData["Products"] = new SelectList(_context.Prodotti.Where(p => p.TipoProdotto == "Comune"), "IdProdotto", "NomeCompleto");

            return View(vendita);
        }



        // GET: Vendita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendita = await _context.Vendite.FindAsync(id);
            if (vendita == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CodiceFiscale", vendita.IdCliente);
            return View(vendita);
        }

        // POST: Vendita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVendita,IdCliente,DataVendita,PrezzoTotale")] Vendita vendita)
        {
            if (id != vendita.IdVendita)
            {
                return NotFound();
            }

            ModelState.Remove("DettagliVendite");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenditaExists(vendita.IdVendita))
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
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "IdCliente", "CodiceFiscale", vendita.IdCliente);
            return View(vendita);
        }

        // GET: Vendita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendita = await _context.Vendite
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdVendita == id);
            if (vendita == null)
            {
                return NotFound();
            }

            return View(vendita);
        }

        // POST: Vendita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendita = await _context.Vendite.FindAsync(id);
            if (vendita != null)
            {
                _context.Vendite.Remove(vendita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenditaExists(int id)
        {
            return _context.Vendite.Any(e => e.IdVendita == id);
        }
    }
}
