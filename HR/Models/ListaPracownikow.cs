using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SystemHR.Models
{
    public class ListaPracownikow
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę podaj imię")]
        [MinLength(2, ErrorMessage = "Imię musi mieć co najmniej 2 znaki")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Proszę podaj nazwisko")]
        [MinLength(2, ErrorMessage = "Nazwisko musi mieć co najmniej 2 znaki")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Proszę podaj adres e-mail")]
        [EmailAddress(ErrorMessage = "Proszę podaj poprawny adres e-mail")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Proszę podaj poprawny numer telefonu")]
        public string Telefon { get; set; }

        public int Wziete { get; set; }
    }
}
