using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Proszę podaj imię")]
        public string Imie { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podaj nazwisko")]
        public string Nazwisko { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podaj email")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podaj hasło")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$", ErrorMessage = "Hasło musi zawierać co najmniej jedną dużą literę, jedną małą literę i jedną cyfrę")]
        public string Haslo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę potwierdź hasło")]
        [Compare("Haslo", ErrorMessage = "Hasła nie są zgodne")]
        public string ConfirmHaslo { get; set; } = string.Empty;
    }
}
