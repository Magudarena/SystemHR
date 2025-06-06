using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class ListaUrlopow
    {
        [Key]
        public int Id { get; set; }
        public string Nr_identyfikacyjny { get; set; }

        [Required(ErrorMessage = "Proszę podaj nazwę")]
        [MinLength(2, ErrorMessage = "Nazwa musi mieć co najmniej 2 znaki")]
        public string nazwa_wolnego { get; set; }

        [Required(ErrorMessage = "Proszę podaj autora")]
        [MinLength(2, ErrorMessage = "dane_wolnego musi mieć co najmniej 2 znaki")]
        public string dane_wolnego { get; set; }

        [Required(ErrorMessage = "Proszę podaj Identyfikator")]
        [MinLength(10, ErrorMessage = "Identyfikator musi mieć co najmniej 10 znaków")]
        public string Identyfikator { get; set; }

        public string Kategoria { get; set; }

        public bool Dostepne { get; set; }
    }
}
