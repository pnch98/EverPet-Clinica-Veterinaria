using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Sanitario.Data;
using Sanitario.Models;
using System.Security.Claims;

namespace Sanitario.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        public LoginController(ApplicationDbContext context, IAuthenticationSchemeProvider schemeProvider)
        {
            _context = context;
            _schemeProvider = schemeProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Index")]
        [HttpPost]
        public async Task<IActionResult> Login(Dipendente dipendente)
        {
            // Query per trovare l'dipendente nel db
            var dbUser = _context.Dipendenti.FirstOrDefault(d => d.Username == dipendente.Username);

            // Se la query non trova niente ci restituisce il temp data da stampare nella view
            if (dbUser == null)
            {
                TempData["error"] = "Questo Nome Utente non esiste";
                return View();
            }
            if (dbUser.Password != dipendente.Password)
            {
                TempData["error"] = "Credenziali non valide";
                return View();
            }
            // Trovato l'dipendente, se la password che inseriamo coincide con quella presente sul db possiamo procedere
            // Salviamo nei claims le informazioni sull'dipendente autenticato
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbUser.Username),
                new Claim(ClaimTypes.Role, dbUser.Ruolo),
                new Claim(ClaimTypes.NameIdentifier, dbUser.IdDipendente.ToString())
            };
            // Salviamo in questa variabile l'identità dell'dipendente autenticato
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            TempData["success"] = "Login effettuato con successo";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["success"] = "Logout effettuato con successo";
            return RedirectToAction("Index", "Home");
        }
    }
}
