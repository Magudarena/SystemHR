using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SystemHR.Models;

namespace SystemHR.Controllers
{
    public class KategorieController : Controller
    {
        private readonly SystemHRContext _context;

        public KategorieController(SystemHRContext context)
        {
            _context = context;
        }

        // GET: Kategorie
        public IActionResult Index()
        {
            var kategorie = _context.Kategoria.ToList();
            return View("Kategorie", kategorie); // Wskazanie widoku Kategorie.cshtml
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            return View(); // Widok formularza dodawania kategorii
        }

        [HttpPost]
        public IActionResult Dodaj(Kategoria model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Kategoria.Add(model); // Dodanie nowej kategorii do bazy danych
                    _context.SaveChanges(); // Zapisanie zmian w bazie danych
                    return RedirectToAction("Index"); // Powrót do listy kategorii
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE KEY"))
                    {
                        ModelState.AddModelError(string.Empty, "Wybrana kategoria już istnieje.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas zapisywania zmian w bazie danych.");
                    }
                }
            }

            return View(model); // Jeśli model jest niepoprawny, ponownie wyświetl formularz z błędami walidacji
        }

        [HttpGet]
        public IActionResult Usun(int id)
        {
            var kategoria = _context.Kategoria.FirstOrDefault(k => k.Id == id);
            if (kategoria == null)
            {
                return NotFound(); // Jeśli kategoria nie istnieje
            }

            return View(kategoria); // Przekaż kategorię do widoku
        }

        [HttpPost]
        public IActionResult UsunPotwierdzenie(int id)
        {
            var kategoria = _context.Kategoria.FirstOrDefault(k => k.Id == id);
            if (kategoria == null)
            {
                return NotFound(); // Jeśli kategoria nie istnieje
            }

            try
            {
                _context.Kategoria.Remove(kategoria); // Usuń kategorię z bazy danych
                _context.SaveChanges(); // Zapisz zmiany
                TempData["Success"] = "Kategoria została pomyślnie usunięta.";
                return RedirectToAction("Index"); // Powrót na listę kategorii
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    ModelState.AddModelError(string.Empty, "Do kategorii jest przypisany co najmniej jeden element. Brak możliwości usunięcia");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas zapisywania zmian w bazie danych.");
                }
            }

            var kategorie = _context.Kategoria.ToList();
            return View("Kategorie", kategorie);
        }
    }
}