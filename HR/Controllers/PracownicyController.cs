using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SystemHR.Models;

namespace SystemHR.Controllers
{
    public class PracownicyController : Controller
    {
        private readonly SystemHRContext _context;

        public PracownicyController(SystemHRContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var pracownicy = _context.ListaPracownikow.ToList(); // Pobierz listę Pracowników z bazy danych
            return View("ListaPracownikow", pracownicy); // Wskaż widok ListaPracownikow
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            return View("~/Views/Pracownicy/NowyPracownik.cshtml");
        }


        [HttpPost]
        public IActionResult Dodaj(Pracownik model)
        {
            if (ModelState.IsValid)
            {
                // Zapisz nowego Pracownika w tabeli Pracownik
                try
                {
                    _context.Pracownik.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE KEY"))
                    {
                        ModelState.AddModelError(string.Empty, "Podany Pracownik już istnieje. Sprawdź dane Pracownika w zakładce 'Pracownicy'.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas zapisywania zmian w bazie danych.");
                    }
                }
            }

            return View("NowyPracownik", model);
        }

        // GET: Potwierdzenie usunięcia Pracownika
        [HttpGet]
        public IActionResult Usun(int id)
        {
            var Pracownik = _context.Pracownik.FirstOrDefault(k => k.Id == id);
            if (Pracownik == null)
            {
                return NotFound();
            }

            return View(Pracownik); // Przekazanie obiektu NowyPracownik do widoku
        }

        // POST: Usunięcie Pracownika
        [HttpPost]
        public IActionResult UsunPotwierdzenie(int id)
        {
            var Pracownik = _context.Pracownik.FirstOrDefault(k => k.Id == id);
            if (Pracownik == null)
            {
                return NotFound();
            }

            _context.Pracownik.Remove(Pracownik); // Usuń Pracownika z tabeli
            _context.SaveChanges(); // Zapisz zmiany w bazie danych

            return RedirectToAction("Index"); // Przekierowanie na listę Pracowników
        }


        [HttpGet]
        public IActionResult Edytuj(int id)
        {
            // Pobranie Pracownika z bazy danych
            var Pracownik = _context.Pracownik.FirstOrDefault(k => k.Id == id);
            if (Pracownik == null)
            {
                return NotFound(); // Jeśli Pracownik nie istnieje
            }

            return View(Pracownik); // Przekazanie Pracownika do widoku
        }

        [HttpPost]
        public IActionResult Edytuj(Pracownik model)
        {
            if (ModelState.IsValid)
            {
                // Pobierz Pracownika z bazy danych
                var Pracownik = _context.Pracownik.FirstOrDefault(k => k.Id == model.Id);
                if (Pracownik == null)
                {
                    return NotFound();
                }

                // Aktualizacja danych Pracownika
                Pracownik.Imie = model.Imie;
                Pracownik.Nazwisko = model.Nazwisko;
                Pracownik.Telefon = model.Telefon;
                Pracownik.Email = model.Email;

                _context.SaveChanges(); // Zapisz zmiany w bazie danych
                return RedirectToAction("Index"); // Przekierowanie na listę Pracowników
            }

            return View(model); // Jeśli model jest nieprawidłowy, wyświetl formularz z błędami
        }

        public IActionResult UrlopyPracownika(int id)
        {
            try
            {
                var urlopy = _context.UrlopPerPracownik
                    .Where(k => k.Id_Pracownik == id)
                    .Select(k => new UrlopPerPracownik
                    {
                        Id_Wolne = k.Id_Wolne,
                        Id_Urlop = k.Id_Urlop,
                        Id_Pracownik = k.Id_Pracownik,
                        Nr_identyfikacyjny = k.Nr_identyfikacyjny,
                        nazwa_wolnego = k.nazwa_wolnego,
                        dane_wolnego = k.dane_wolnego,
                        Identyfikator = k.Identyfikator,
                        Poczatek_Wolnego = k.Poczatek_Wolnego,
                        Koniec_Wolnego = k.Koniec_Wolnego
                    })
                    .ToList();

                var Pracownik = _context.Pracownik.FirstOrDefault(k => k.Id == id);
                if (Pracownik != null)
                {
                    ViewBag.PracownikImieNazwisko = $"{Pracownik.Imie} {Pracownik.Nazwisko}";
                }
                else
                {
                    ViewBag.PracownikImieNazwisko = "Nieznany Pracownik";
                }

                return View(urlopy);
            }
            catch (Exception)
            {
                ViewBag.Message = "Brak urlopów przypisanych do tego Pracownika.";
                return View(new List<UrlopPerPracownik>());
            }
        }

    }
}