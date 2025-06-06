using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class LogowanieModel
    {
        [Required(ErrorMessage = "Proszę podaj email")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podaj hasło")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków")]
        public string Haslo { get; set; }
    }
}
