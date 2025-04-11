using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SystemHR.Models;

namespace SystemHR.Controllers
{
    public class UrlopyController : Controller
    {
        private readonly SystemHRContext _context;

        public UrlopyController(SystemHRContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Pobierz listę z bazy danych
            var urlopy = _context.Urlop.ToList();
            return View("ListaUrlopow", urlopy);
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            var kategorie = _context.Kategoria.ToList();
            ViewBag.Kategorie = new SelectList(kategorie, "Id", "Nazwa");
            return View("NowyUrlop");
        }

        // POST: Obsługuje zapis danych
        [HttpPost]
        public IActionResult Dodaj(Urlop model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.NowyUrlop.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE KEY"))
                    {
                        ModelState.AddModelError(string.Empty, "Numer identyfikacyjny został już zarezerwowany. Wybierz inny numer.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas zapisywania zmian w bazie danych.");
                    }
                }
            }

            var kategorie = _context.Kategoria.ToList();
            ViewBag.Kategorie = new SelectList(kategorie, "Id", "Nazwa");
            return View("NowyUrlop", model);
        }

        // GET: Potwierdzenie usunięcia
        [HttpGet]
        public IActionResult Usun(int id)
        {
            // Pobierz z tabeli NowyUrlop
            var urlop = _context.NowyUrlop.FirstOrDefault(k => k.Id == id);
            if (urlop == null)
            {
                return NotFound();
            }

            return View(urlop); // Przekazanie modelu do widoku
        }

        [HttpPost]
        public IActionResult UsunPotwierdzenie(int id)
        {
            // Pobierz z tabeli NowyUrlop
            var urlop = _context.NowyUrlop.FirstOrDefault(k => k.Id == id);
            if (urlop == null)
            {
                return NotFound();
            }


            _context.NowyUrlop.Remove(urlop); // Usuń z tabeli
            _context.SaveChanges(); // Zapisz zmiany w bazie danych

            return RedirectToAction("Index"); // Przekierowanie na listę 
        }


        [HttpGet]
        public IActionResult Edytuj(int id)
        {
            // Pobierz z bazy danych
            var urlop = _context.NowyUrlop.FirstOrDefault(k => k.Id == id);
            if (urlop == null)
            {
                return NotFound();
            }

            // Pobierz listę kategorii z bazy danych



            var kategorie = _context.Kategoria.Select(k => new SelectListItem
            {
                Value = k.Id.ToString(),
                Text = k.Nazwa
            }).ToList();

            // Przekaż listę kategorii do widoku
            ViewBag.Kategorie = kategorie;

            return View(urlop); // Przekaż do widoku
        }

        [HttpPost]
        public IActionResult Edytuj(Urlop model)
        {
            if (ModelState.IsValid)
            {
                // Pobierz z bazy danych
                var urlop = _context.NowyUrlop.FirstOrDefault(k => k.Id == model.Id);
                if (urlop == null)
                {
                    return NotFound();
                }

                // Aktualizacja danych
                urlop.Nr_identyfikacyjny = model.Nr_identyfikacyjny;
                urlop.nazwa_wolnego = model.nazwa_wolnego;
                urlop.dane_wolnego = model.dane_wolnego;
                urlop.Identyfikator = model.Identyfikator;
                urlop.Kategoria = model.Kategoria;
                urlop.Dostepne = model.Dostepne;

                _context.SaveChanges(); // Zapisz zmiany w bazie danych
                return RedirectToAction("Index");
            }

            return View(model); // Wyświetl formularz z błędami walidacji
        }

        [HttpGet]
        public IActionResult Wroc(int id)
        {
            // Pobierz 
            var BranieWolnego = _context.Wolne
                .FirstOrDefault(w => w.Id == id && w.Koniec_Wolnego == null);

            if (BranieWolnego == null)
            {
                return NotFound("Nie znaleziono wpisu lub urlop został już zakończony.");
            }

            // Pobierz z mapowania NowyUrlop
            var urlop = _context.NowyUrlop
                .FirstOrDefault(k => k.Id == BranieWolnego.Id_Urlop);

            if (urlop == null)
            {
                return NotFound("Nie znaleziono wpisu w bazie danych.");
            }

            // Przekaż dane do widoku
            ViewBag.BranieWolnego = BranieWolnego;
            ViewBag.Urlop = urlop;

            return View();
        }



        [HttpPost]
        public IActionResult WrocPotwierdzenie(int id_wolnego)
        {
            // Znajdź wolne na podstawie ID
            var BranieWolnego = _context.Wolne
                .FirstOrDefault(w => w.Id == id_wolnego && w.Koniec_Wolnego == null);

            if (BranieWolnego == null)
            {
                return NotFound("Nie znaleziono rekordu lub urlop został już zakończony.");
            }

            // Ustaw datę powrotu
            BranieWolnego.Koniec_Wolnego = DateTime.Now;

            // Znajdź z mapowania NowyUrlop i ustaw jako dostępną
            var urlop = _context.NowyUrlop.FirstOrDefault(k => k.Id == BranieWolnego.Id_Urlop);
            if (urlop != null)
            {
                urlop.Dostepne = true;
            }

            // Zapisz zmiany
            _context.SaveChanges();

            TempData["Message"] = "Powrót został pomyślnie zarejestrowany.";
            return RedirectToAction("UrlopyPracownika", "Pracownicy", new { id = BranieWolnego.Id_Pracownik });
        }
    }
}