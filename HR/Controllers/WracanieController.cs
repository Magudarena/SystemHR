using Microsoft.AspNetCore.Mvc;
using SystemHR.Models;

namespace SystemHR.Controllers
{
    public class WracanieController : Controller
    {
        private readonly SystemHRContext _context;

        public WracanieController(SystemHRContext context)
        {
            _context = context;
        }

        public IActionResult Wracanie(int id)
        {

            return View();
        }


        [HttpGet]
        public IActionResult Wroc(int id)
        {
           
            var BranieWolnego = _context.Wolne
                .FirstOrDefault(w => w.Id == id && w.Koniec_Wolnego == null);

            if (BranieWolnego == null)
            {
                return NotFound("Nie znaleziono elementu lub urlop został już zakończony.");
            }

            var urlop = _context.NowyUrlop
                .FirstOrDefault(k => k.Id == BranieWolnego.Id_Urlop);

            if (urlop == null)
            {
                return NotFound("Nie znaleziono urlopu w bazie danych.");
            }

            // Przekazanie danych do widoku
            ViewBag.BranieWolnego = BranieWolnego;
            ViewBag.Urlop = urlop;

            return View();
        }

        [HttpPost]
        public IActionResult PrzekierujDoPowrotu([Bind("NrIdentyfikacyjny")] string nrIdentyfikacyjny)
        {
            if (string.IsNullOrEmpty(nrIdentyfikacyjny))
            {
                ViewData["Message"] = "Numer identyfikacyjny nie został wprowadzony.";
                return View("Wracanie");
            }

            Console.WriteLine($"Przekazany numer identyfikacyjny: {nrIdentyfikacyjny}");

            var urlop = _context.UrlopPerPracownik
                .FirstOrDefault(k => k.Nr_identyfikacyjny == nrIdentyfikacyjny && k.Koniec_Wolnego == null);

            if (urlop == null)
            {
                Console.WriteLine("Nie znaleziono urlopu w bazie danych.");
                ViewData["Message"] = "Nie znaleziono urlopu do zakończenia lub urlop już został zakończony.";
                return View("Wracanie");
            }

            Console.WriteLine($"Znaleziono urlop: {urlop.nazwa_wolnego}, ID identyfikacyjny: {urlop.Id_Wolne}");

            // Przekierowanie do widoku Wroc w kontrolerze Urlop
            return RedirectToAction("Wroc", "Wracanie", new { id = urlop.Id_Wolne });
        }
    }
}