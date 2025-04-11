using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SystemHR.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SystemHR.Controllers
{
    public class WolneController : Controller
    {
        private readonly SystemHRContext _context;

        public WolneController(SystemHRContext context)
        {
            _context = context;
        }

        // Wyświetla widok formularza wyszukiwania 
        public IActionResult Index()
        {
            return View("Wolne");
        }

        // Wyszukuje po numerze identyfikacyjnym
        [HttpPost]
        public IActionResult SzukajUlropu(string nrIdentyfikacyjny)
        {

            var urlop = _context.NowyUrlop.FirstOrDefault(k => k.Nr_identyfikacyjny == nrIdentyfikacyjny && k.Dostepne);

            if (urlop == null)
            {
                ViewBag.Message = "Nie znaleziono urlopu lub jest niedostępny.";
                return View("Wolne");
            }

            Console.WriteLine($"Znaleziono urlop: Id = {urlop.Id}, Tytuł = {urlop.nazwa_wolnego}");
            return View("PodsumowanieUrlopu", urlop);
        }

        [HttpPost]
        public IActionResult SzukajPracownika(string telefon, int urlopId)
        {

            var Pracownik = _context.Pracownik.FirstOrDefault(k => k.Telefon == telefon);

            if (Pracownik == null)
            {
                ViewBag.Message = "Nie znaleziono Pracownika o podanym numerze telefonu.";
                var urlop = _context.NowyUrlop.FirstOrDefault(k => k.Id == urlopId);
                return View("PodsumowanieUrlopu", urlop);
            }

            ViewBag.UrlopId = urlopId;
            return View("WybierzPracownika", new List<Pracownik> { Pracownik });
        }






        public IActionResult WolneZListy(int id)
        {
            // Pobierz z bazy danych
            var urlop = _context.NowyUrlop.Select(k => new SelectListItem
            {
                Value = k.Id.ToString(),
                Text = k.nazwa_wolnego
            }).ToList();
            if (urlop == null)
            {
                return NotFound();
            }


            // Przekaż listę kategorii do widoku
            ViewBag.nazwa_wolnego = urlop;

            return View(urlop); // Przekaż do widoku
        }






        [HttpGet]
        public async Task<JsonResult> GetAvailableWolne()
        {
            var wolneList = await _context.Urlop
                .Select(u => new { value = u.Id, text = u.nazwa_wolnego })
                .ToListAsync();

            return Json(wolneList);
        }






        [HttpPost]
        public IActionResult WezWolne(int urlopId, int PracownikId)
        {
            // Znajdź po ID w odpowiednim DbSet
            var urlop = _context.NowyUrlop.FirstOrDefault(k => k.Id == urlopId);


            // Aktualizacja statusu urlopu
            urlop.Dostepne = false;
            _context.NowyUrlop.Update(urlop);

            // Dodanie rekordu do tabeli wypożyczenia
            var BranieWolnego = new BranieWolnego
            {
                Id_Urlop = urlopId,
                Id_Pracownik = PracownikId,
                Poczatek_Wolnego = DateTime.Now,
                Koniec_Wolnego = null
            };
            _context.Wolne.Add(BranieWolnego);

            // Zapisanie zmian w bazie
            _context.SaveChanges();

            // Przekierowanie do widoku Listy urlopów Pracownika
            return RedirectToAction("UrlopyPracownika", "Pracownicy", new { id = PracownikId });
        }

    }
}
