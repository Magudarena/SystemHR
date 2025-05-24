using System.ComponentModel.DataAnnotations;

namespace SystemHR.Models
{
    public class ListaUrlopow
    {
        [Key]
        public int Id { get; set; }

        public string Nr_identyfikacyjny { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podaj nazwę")]
        [MinLength(2, ErrorMessage = "Nazwa musi mieć co najmniej 2 znaki")]
        public string nazwa_wolnego { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podaj autora")]
        [MinLength(2, ErrorMessage = "dane_wolnego musi mieć co najmniej 2 znaki")]
        public string dane_wolnego { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podaj Identyfikator")]
        [MinLength(10, ErrorMessage = "Identyfikator musi mieć co najmniej 10 znaków")]
        public string Identyfikator { get; set; } = string.Empty;

        public string Kategoria { get; set; } = string.Empty;

        public bool Dostepne { get; set; }
    }
}
