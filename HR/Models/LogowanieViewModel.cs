using System.ComponentModel.DataAnnotations;

namespace SystemHR.Models
{
    public class LogowanieViewModel
    {
        [Required(ErrorMessage = "Proszę podaj email")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proszę podaj hasło")]
        public string Haslo { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
