using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class Urlop
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę podać nr identyfikacyjny")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Nr identyfikacyjny musi mieć dokładnie 10 cyfr")]
        public string Nr_identyfikacyjny { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę wolnego")]
        public string nazwa_wolnego { get; set; }

        [Required(ErrorMessage = "Proszę podać dane wolnego")]
        public string dane_wolnego { get; set; }

        [Required(ErrorMessage = "Proszę podać identyfikator")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Identyfikator musi mieć dokładnie 13 cyfr")]
        public string Identyfikator { get; set; }

        public bool Dostepne { get; set; } = true;

        [Required(ErrorMessage = "Proszę wybrać kategorię")]
        public int Kategoria { get; set; }
    }
}
