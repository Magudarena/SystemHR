using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SystemHR.Models;

namespace SystemHR.Controllers
{
    [AllowAnonymous]
    public class LogowanieController : Controller
    {
        private readonly SystemHRContext _context;

        public LogowanieController(SystemHRContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Logowanie()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logowanie(LogowanieViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Wyszukanie użytkownika na podstawie e-maila
                var PracownikHR = _context.Uzytkownicy.FirstOrDefault(u => u.Email == model.Email);
                if (PracownikHR != null)
                {
                    // Weryfikacja hasła
                    var hasher = new PasswordHasher<PracownikHR>();
                    var wynik = hasher.VerifyHashedPassword(null, PracownikHR.Haslo, model.Haslo);

                    if (wynik == PasswordVerificationResult.Success)
                    {
                        // Logowanie zakończone sukcesem
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, PracownikHR.Email),
                            new Claim("Imie", PracownikHR.Imie),
                            new Claim("Nazwisko", PracownikHR.Nazwisko),
                            new Claim("Uprawnienia", PracownikHR.Id_Uprawnienia.ToString() ?? "3")
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        TempData["Success"] = "Zalogowano pomyślnie!";
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Błąd logowania
                ModelState.AddModelError(string.Empty, "Nieprawidłowy email lub hasło");
            }

            return View(model);
        }

        public async Task<IActionResult> Wyloguj()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Success"] = "Wylogowano pomyślnie!";
            return RedirectToAction("Index", "Home");
        }
    }
}
