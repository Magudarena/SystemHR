using System.ComponentModel.DataAnnotations;

namespace SystemHR.Models
{
    public class Urlop
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę podać nr identyfikacyjny")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Nr identyfikacyjny musi mieć dokładnie 10 cyfr")]
        public string Nr_identyfikacyjny { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podać nazwę wolnego")]
        public string nazwa_wolnego { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podać dane wolnego")]
        public string dane_wolnego { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podać identyfikator")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Identyfikator musi mieć dokładnie 13 cyfr")]
        public string Identyfikator { get; set; } = string.Empty;

        public bool Dostepne { get; set; } = true;

        [Required(ErrorMessage = "Proszę wybrać kategorię")]
        [Range(1, 5, ErrorMessage = "Kategoria musi być z zakresu 1–5")]
        public int Kategoria { get; set; }
    }
}
