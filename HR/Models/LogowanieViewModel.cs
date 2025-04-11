using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SystemHR.Models
{
    public class LogowanieViewModel
    {
        [Required(ErrorMessage = "Proszę podaj email")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podaj hasło")]
        public string Haslo { get; set; }

        public bool RememberMe { get; set; }
    }
}