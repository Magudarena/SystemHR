using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SystemHR.Models
{
    public class Pracownik
    {
        public int Id { get; set; } // Klucz główny

        [Required(ErrorMessage = "Proszę podaj imię")]
        [MinLength(2, ErrorMessage = "Imię musi mieć co najmniej 2 znaki")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Proszę podaj nazwisko")]
        [MinLength(2, ErrorMessage = "Nazwisko musi mieć co najmniej 2 znaki")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Proszę podaj adres e-mail")]
        [EmailAddress(ErrorMessage = "Proszę podaj poprawny adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podaj numer telefonu")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Telefon musi zawierać dokładnie 9 cyfr.")]
        public string Telefon { get; set; }

    }
}
