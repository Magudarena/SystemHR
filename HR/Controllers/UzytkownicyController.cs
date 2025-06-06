using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using SystemHR.Models;

namespace SystemHR.Controllers
{
    public class UzytkownicyController : Controller
    {
        private readonly SystemHRContext _context;

        public UzytkownicyController(SystemHRContext context)
        {
            _context = context;
        }

        // Wyświetlenie listy użytkowników - tylko dla administratorów
        [Authorize]
        public IActionResult Uzytkownicy()
        {
            var uprawnienia = User.FindFirst("Uprawnienia")?.Value;

            if (uprawnienia != "1")
            {
                return Forbid();
            }

            var uzytkownicy = _context.Uzytkownicy
                .Include(u => u.Uprawnienia)
                .ToList();

            var listaUprawnien = _context.Uprawnienia.ToList();
            ViewBag.Uprawnienia = new SelectList(listaUprawnien, "Id", "Nazwa");

            return View(uzytkownicy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ZmienUprawnienia(int id, int noweUprawnienia)
        {
            var PracownikHR = _context.Uzytkownicy.Find(id);

            if (PracownikHR == null)
            {
                return NotFound();
            }

            PracownikHR.Id_Uprawnienia = noweUprawnienia;

            _context.SaveChanges();

            TempData["Success"] = "Poziom uprawnień został pomyślnie zmieniony.";
            return RedirectToAction("Uzytkownicy");
        }
    }
}
